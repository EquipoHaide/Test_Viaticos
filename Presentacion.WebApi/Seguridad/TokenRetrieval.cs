using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Presentacion.WebApi.Seguridad
{
    /// <summary>
    /// Defines some common token retrieval strategies
    /// </summary>
    public static class TokenRetrieval
    {
        /// <summary>
        /// Reads the token from the authrorization header or a query string paramete.
        /// </summary>
        /// <param name="scheme">The scheme (defaults to Bearer).</param>
        /// <param name="name">The name (defaults to access_token).</param>
        /// <returns></returns>
        public static Func<HttpRequest, string> Mixed(string scheme = "Bearer", string name = "access_token")
        {
            return (request) =>
            {
                string authorization = request.Headers["Authorization"].FirstOrDefault();

                if (!string.IsNullOrEmpty(authorization))
                {
                    if (authorization.StartsWith(scheme + " ", StringComparison.OrdinalIgnoreCase))
                    {
                        return authorization.Substring(scheme.Length + 1).Trim();
                    }
                }

                return request.Query[name].FirstOrDefault();
            };
        }
    }
}