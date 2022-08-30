using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dominio.Seguridad.Entidades
{
    [Table("Accesos", Schema = "Seguridad")]
    public class Acceso : Seguimiento, IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdAccion { get; set; }
        [Required]
        public int IdRol { get; set; }
        [Required]
        public DateTime FechaCaducidad { get; set; }

        [ForeignKey("IdRol")]
        public Rol Rol { get; set; }
        [ForeignKey("IdAccion")]
        public Accion Accion { get; set; }
    }
}
