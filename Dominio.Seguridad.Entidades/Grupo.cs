using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("Grupos", Schema = "Seguridad")]
    public class Grupo : RecursoBase<RecursoGrupo>
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(150)]
        public string Descripcion { get; set; }

        public List<UsuarioGrupo> Usuarios { get; set; }
        public List<RolGrupo> Roles { get; set; }
    }
}
