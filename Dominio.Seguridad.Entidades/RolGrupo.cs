using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("RolesGrupo", Schema = "Seguridad")]
    public class RolGrupo : SeguimientoCreacion, IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdRol { get; set; }
        [Required]
        public int IdGrupo { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
        [ForeignKey("IdGrupo")]
        public Grupo Grupo { get; set; }
    }
}
