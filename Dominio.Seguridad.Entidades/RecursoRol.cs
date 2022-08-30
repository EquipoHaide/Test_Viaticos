using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("RecursosRol", Schema = "Seguridad")]
    public class RecursoRol : Permiso
    {

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }

        [ForeignKey("IdRecurso")]
        public Rol Recurso { get; set; }
    }
}
