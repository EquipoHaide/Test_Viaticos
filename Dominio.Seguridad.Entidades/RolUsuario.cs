using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("RolUsuarios", Schema = "Seguridad")]
    public class RolUsuario : SeguimientoCreacion, IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdRol { get; set; }


        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
    }
}
