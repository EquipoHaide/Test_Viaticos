using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("UsuariosGrupo", Schema = "Seguridad")]
    public class UsuarioGrupo : SeguimientoCreacion, IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdGrupo { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
        [ForeignKey("IdGrupo")]
        public Grupo Grupo { get; set; }
    }
}
