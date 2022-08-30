using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Infraestructura.Transversal.Autenticacion
{
    public class TokenManager : ITokenManager
    {
        List<AccessToken> AccessTokensList { get; set; }
        string Autority { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        bool RequireHttps { get; set; }
        //DiscoveryClient Discoveryclient { get; set; }
        TokenClient TokenClient { get; set; }
        private DateTime LastClean { get; set; }
        private int CleanIntervalInSeconds { get; set; }

        public TokenManager()
        {
            AccessTokensList = new List<AccessToken>();
        }

        public async Task ConfigureAsync(string autority, string clientId, string clientSecret, bool requireHttps = true, int cleanIntervalInSeconds = 1800)
        {
            Autority = autority;
            ClientId = clientId;
            ClientSecret = clientSecret;
            RequireHttps = requireHttps;
            CleanIntervalInSeconds = cleanIntervalInSeconds;
            LastClean = DateTime.Now;

            await SetTokenClient();
        }

        /// <summary>
        /// Gets the user's info
        /// </summary>
        public async Task<List<Claim>> GetUserInfoAsync(string accessToken)
        {
            var client = new HttpClient();

            var discoveryDocumentRequest = new DiscoveryDocumentRequest
            {
                Address = Autority,
                Policy = new DiscoveryPolicy()
                {
                    RequireHttps = RequireHttps
                }
            };


            var discovery = await client.GetDiscoveryDocumentAsync(discoveryDocumentRequest);

            if (discovery.IsError)
                return new List<Claim>();

            UserInfoRequest userInfoRequest = new UserInfoRequest
            {
                Address = discovery.UserInfoEndpoint,
                Token = accessToken
            };

            var userInfoResponse = await client.GetUserInfoAsync(userInfoRequest);

            if (userInfoResponse.IsError)
                return new List<Claim>();

            return userInfoResponse.Claims.ToList();
        }

        /// <summary>
        /// Gets a access token
        /// </summary>
        public async Task<string> GetTokenAsync(string scope, string subjectId, string token = "")
        {
            AccessToken accessToken = null;

            if (AccessTokensList.Any())
            {
                accessToken = AccessTokensList.Find(ac => ac.Scope == scope && ac.SubjectId == subjectId);

                if (accessToken != null)
                {
                    if (accessToken.ExpireIn > DateTime.Now)
                        return accessToken.Token;

                    if (!string.IsNullOrEmpty(accessToken.RefreshToken) && !string.IsNullOrWhiteSpace(accessToken.RefreshToken))
                    {
                        var newAccessToken = await GetTokenByRefreshAsync(accessToken.RefreshToken);

                        if (newAccessToken != null)
                        {
                            accessToken.Token = newAccessToken.Token;
                            accessToken.RefreshToken = newAccessToken.RefreshToken;
                            accessToken.ExpireIn = newAccessToken.ExpireIn;

                            return accessToken.Token;
                        }

                        AccessTokensList.Remove(accessToken);
                        accessToken = null;
                    }
                }
            }

            accessToken = (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
                ? await GetNewTokenAsync(scope, subjectId)
                : await GetNewTokenByDelegationAsync(scope, subjectId, token);

            if (accessToken != null)
            {
                AccessTokensList.Add(accessToken);
                return accessToken.Token;
            }

            _ = UpdateAccessTokensList();

            return null;
        }

        private async Task<AccessToken> GetNewTokenAsync(string scope, string subject)
        {
            if (TokenClient == null)
                await SetTokenClient();

            AccessToken accessToken = null;

            var tokenResponse = await TokenClient.RequestClientCredentialsTokenAsync(scope);

            if (!tokenResponse.IsError)
            {
                accessToken = new AccessToken()
                {
                    SubjectId = subject,
                    Token = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    ExpireIn = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
                    Scope = scope
                };
            }

            return accessToken;
        }

        private async Task<AccessToken> GetNewTokenByDelegationAsync(string scope, string subject, string token)
        {
            if (TokenClient == null)
                await SetTokenClient();

            AccessToken accessToken = null;

            //var BearerToken = new { token };
            //var tokenResponse = await TokenClient.RequestCustomGrantAsync("delegation", scope, BearerToken);
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("scope", scope);
            parameters.Add("token", token);
            var tokenResponse = await TokenClient.RequestTokenAsync("delegation", parameters);

            if (!tokenResponse.IsError)
            {
                accessToken = new AccessToken()
                {
                    SubjectId = subject,
                    Token = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    ExpireIn = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
                    Scope = scope
                };
            }

            return accessToken;
        }

        private async Task<AccessToken> GetTokenByRefreshAsync(string refreshToken)
        {
            if (TokenClient == null)
                await SetTokenClient();

            AccessToken accessToken = null;

            var tokenResponse = await TokenClient.RequestRefreshTokenAsync(refreshToken);

            if (!tokenResponse.IsError)
            {
                accessToken = new AccessToken()
                {
                    Token = tokenResponse.AccessToken,
                    RefreshToken = tokenResponse.RefreshToken,
                    ExpireIn = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn),
                };
            }

            return accessToken;
        }

        private async Task SetTokenClient()
        {
            var client = new HttpClient();

            var discoveryDocumentRequest = new DiscoveryDocumentRequest
            {
                Address = Autority,
                Policy = new DiscoveryPolicy()
                {
                    RequireHttps = RequireHttps
                }
            };

            var discovery = await client.GetDiscoveryDocumentAsync(discoveryDocumentRequest);

            if (!discovery.IsError)
            {
                TokenClientOptions options = new TokenClientOptions
                {
                    Address = discovery.TokenEndpoint,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret
                };

                TokenClient = new TokenClient(client, options);
            }

        }

        private Task UpdateAccessTokensList()
        {
            if (LastClean.AddSeconds(CleanIntervalInSeconds) < DateTime.Now)
            {
                LastClean = DateTime.Now;

                return Task.Run(() => {

                    if (AccessTokensList != null && AccessTokensList.Any())
                    {
                        var removables = AccessTokensList.Where(c => c.ExpireIn <= DateTime.Now && (string.IsNullOrEmpty(c.RefreshToken) || string.IsNullOrWhiteSpace(c.RefreshToken)));

                        if (removables.Any())
                        {
                            foreach (var item in removables)
                            {
                                AccessTokensList.Remove(item);
                            }
                        }
                    }
                });
            }

            return Task.CompletedTask;
        }
    }
}