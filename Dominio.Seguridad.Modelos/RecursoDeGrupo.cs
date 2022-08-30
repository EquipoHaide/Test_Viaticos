using System;

namespace Dominio.Seguridad.Modelos
{
    public class RecursoDeGrupo : RecursoDeGrupoBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public bool EsAsignado { get; set; }
    }
}
