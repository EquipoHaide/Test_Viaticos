using Dominio.Nucleo.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("RecursosAccion", Schema = "Seguridad")]
    public class RecursoAccion : Permiso
    {

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }


        [ForeignKey("IdRecurso")]
        public Accion Recurso { get; set; }
    }
}
