using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dominio.Seguridad.Entidades
{
    [Table("OpcionesModulo", Schema = "Seguridad")]
    public class OpcionModulo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdModulo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Activo { get; set; }

        [ForeignKey("IdModulo")]
        public Modulo Modulo { get; set; }
        public List<Accion> Acciones { get; set; }
    }
}
