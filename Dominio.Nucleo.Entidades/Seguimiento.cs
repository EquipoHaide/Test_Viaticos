using System;

namespace Dominio.Nucleo.Entidades
{

    /// <summary>
    /// Representa una entidad que requiere seguimiento completo de usuario. Clave : SG
    /// </summary>
    public class Seguimiento : SeguimientoCreacion, ISeguimiento
    {
        public string IdUsuarioModifico { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string IdUsuarioElimino { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Activo { get; set; }

        public override void Seguir(string subjectId)
        {
            this.Seguir(subjectId, false, false);
        }

        public void Seguir(string subjectId, bool esEliminar)
        {
            this.Seguir(subjectId, false, esEliminar);
        }

        public void Seguir(string subjectId, bool esModificar, bool esEliminar)
        {
            if (!esModificar && !esEliminar)
            {
                base.Seguir(subjectId);
                this.IdUsuarioModifico = subjectId;
                this.FechaModificacion = DateTime.Now;
                this.Activo = true;
            }
            else if (esModificar)
            {
                this.IdUsuarioModifico = subjectId;
                this.FechaModificacion = DateTime.Now;
            }
            else if (esEliminar)
            {
                this.IdUsuarioElimino = subjectId;
                this.FechaEliminacion = DateTime.Now;
                this.Activo = false;
            }
        }
    }
}