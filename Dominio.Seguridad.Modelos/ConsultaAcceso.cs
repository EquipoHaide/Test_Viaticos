using Dominio.Nucleo;
using System;

namespace Dominio.Seguridad.Modelos
{
    public class ConsultaAcceso : IModel
    {
        public int IdRol { get; set; }

        public string Query { get; set; }
        public bool? EsAsignado { get; set; }

        public string Modulo { get; set; }
        public string Opcion { get; set; }
        public string Accion { get; set; }

        public DateTime? Inicio { get; set; }

        private DateTime? fin;
        public DateTime? Fin
        {
            get
            {
                return fin == null ? fin : fin.Value.EndDay();
            }
            set
            {
                fin = value;
            }
        }

        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
