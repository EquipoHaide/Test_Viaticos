using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa una entidad que requiere seguimiento de la creación. Clave : SGC
    /// </summary>
    public class SeguimientoCreacion : ISeguimientoCreacion
    {
        [Required]
        public string IdUsuarioCreo { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }

        public virtual void Seguir(string subjectId)
        {
            this.IdUsuarioCreo = subjectId;
            this.FechaCreacion = DateTime.Now;
        }
    }
}