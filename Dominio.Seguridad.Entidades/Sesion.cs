using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("Sesiones", Schema = "Seguridad")]
    public class Sesion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int TokenCount { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        public DateTime Expira { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
