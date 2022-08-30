using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi.Seguridad
{
    public static class AuthData
    {
        // <summary>
        /// Gets the subject id from the principal.
        /// </summary>
        public static string GetSubjectId(this ControllerBase controller)
        {
            return controller.User.FindFirst(JwtClaimTypes.Subject)?.Value;
        }

        /// <summary>
        /// Gets the clientId id from the principal.
        /// </summary>
        public static string GetClientId(this ControllerBase controller)
        {
            return controller.User.FindFirst(JwtClaimTypes.ClientId)?.Value;
        }

        /// <summary>
        /// Gets the subject id or the the client id from the principal.
        /// </summary>
        public static string GetIdentityId(this ControllerBase controller)
        {
            return controller.GetSubjectId() ?? controller.GetClientId();
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public static string GetAccesToken(this ControllerBase controller)
        {
            return controller.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
        }

        /// <summary>
        /// Get access toke expiration
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static DateTime GetAccessTokenExpiration(this ControllerBase controller)
        {
            var exp = double.Parse(new JwtSecurityTokenHandler().ReadJwtToken(controller.GetAccesToken()).Claims.First(e => e.Type == JwtClaimTypes.Expiration).Value);

            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(exp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Gets the user's info
        /// </summary>
        public static Task<List<Claim>> GetUserInfoAsync(this ControllerBase controller)
        {
            return App.Aplicacion.Instancia.GetTokenManager().GetUserInfoAsync(controller.GetAccesToken());
        }
    }
}