using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("RecursosGrupo", Schema = "Seguridad")]
    public class RecursoGrupo : Permiso
    {

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        [ForeignKey("IdRecurso")]
        public Grupo Recurso { get; set; }
    }
}
