using Dominio.Nucleo;
using System;

namespace Dominio.Seguridad.Modelos
{
    public class ConsultaRecursoGrupoModelo : IModeloConsultaRecurso
    {
        public int IdRol { get; set; }

        public string Query { get; set; }
        public bool? EsAsignado { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool EsLectura { get; set; }
        public bool EsEscritura { get; set; }
        public bool EsEjecucion { get; set; }
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
            throw new NotImplementedException();
        }
    }
}
