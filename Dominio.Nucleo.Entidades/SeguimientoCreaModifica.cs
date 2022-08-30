using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa una entidad que requiere seguimiento de la creación y modificación. Clave : SGCM
    /// </summary>
    public class SeguimientoCreaModifica : SeguimientoCreacion, ISeguimientoCreaModifica
    {
        [Required]
        public string IdUsuarioModifico { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }

        public override void Seguir(string subjectId)
        {
            Seguir(subjectId, false);
        }

        public void Seguir(string subjectId, bool esModificar)
        {
            if (!esModificar) base.Seguir(subjectId);

            IdUsuarioModifico = subjectId;
            FechaModificacion = DateTime.Now;
        }
    }
}