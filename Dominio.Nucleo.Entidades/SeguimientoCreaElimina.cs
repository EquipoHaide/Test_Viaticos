using System;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa una entidad que requiere seguimiento de la creación y eliminación. Clave : SGCE
    /// </summary>
    public class SeguimientoCreaElimina : SeguimientoCreacion, ISeguimientoCreaElimina
    {
        public string IdUsuarioElimino { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Activo { get; set; }


        public override void Seguir(string subjectId)
        {
            this.Seguir(subjectId, false);
        }
        public void Seguir(string subjectId, bool esEliminar)
        {
            if (!esEliminar)
            {
                base.Seguir(subjectId);
                this.Activo = true;
            }
            else
            {
                this.IdUsuarioElimino = subjectId;
                this.FechaEliminacion = DateTime.Now;
                this.Activo = false;
            }
        }
    }
}