using Dominio.Nucleo.Entidades;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Seguridad.Entidades
{
    [Table("Acciones", Schema = "Seguridad")]
    public class Accion : RecursoBase<RecursoAccion>
    {
        [Required]
        public int IdOpcionModulo { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool EsVisible { get; set; }
        [Required]
        public bool EsPrincipal { get; set; }
        [Required]
        public string Ruta { get; set; }

        public List<Acceso> Accesos { get; set; }
        [ForeignKey("IdOpcionModulo")]
        public OpcionModulo OpcionModulo { get; set; }
    }
}
