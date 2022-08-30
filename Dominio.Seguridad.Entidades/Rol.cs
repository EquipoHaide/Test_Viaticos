using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("Roles", Schema = "Seguridad")]
    public class Rol : RecursoBase<RecursoRol>
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(150)]
        public string Descripcion { get; set; }

        public List<RolUsuario> Usuarios { get; set; }
        public List<RolGrupo> Grupos { get; set; }
        public List<Acceso> Accesos { get; set; }

        public List<RecursoRol> RecursosDeRol { get; set; }
    }
}
