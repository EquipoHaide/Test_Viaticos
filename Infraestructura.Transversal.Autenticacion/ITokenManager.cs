using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Infraestructura.Transversal.Autenticacion
{
    public interface ITokenManager
    {
        /// <summary>
        /// Gets the user's info
        /// </summary>
        Task<List<Claim>> GetUserInfoAsync(string accessToken);

        /// <summary>
        /// Gets a access token
        /// </summary>
        Task<string> GetTokenAsync(string scope, string subjectId, string token = "");
    }
}