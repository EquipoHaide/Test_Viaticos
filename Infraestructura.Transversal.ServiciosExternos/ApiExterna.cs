using System.Collections.Generic;

namespace Infraestructura.Transversal.ServiciosExternos
{
    public class ApiExterna
    {
        public string UrlBase { get; set; }

        public string Scopes { get; set; }

        public List<ServicioApi> Servicios { get; set; }
    }

    public class ServicioApi
    {
        public string Descripcion { get; set; }

        public string Uri { get; set; }
    }
}