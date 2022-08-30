using System;

namespace Infraestructura.Transversal.Autenticacion
{
    public class AccessToken
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public DateTime ExpireIn { get; set; }

        public string SubjectId { get; set; }
    }
}