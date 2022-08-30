using System;

namespace Dominio.Seguridad.Modelos
{
    public class RecursoDeAccion : RecursoDeAccionBase
    {
        public string Modulo { get; set; }
        public string Opcion { get; set; }
        public string Accion { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public bool EsAsignado { get; set; }
    }
}
