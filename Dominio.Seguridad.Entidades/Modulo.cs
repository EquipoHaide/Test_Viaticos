using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("Modulos", Schema = "Seguridad")]
    public class Modulo : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; }

        public List<OpcionModulo> Opciones { get; set; }
    }
}
