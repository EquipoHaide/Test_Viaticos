using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("Pasos", Schema = "Viaticos")]
    public class PasoViatico : IPaso
    {
        public int Id { get; set ; }

        [Required]
        public int IdRol { get; set; }

        [Required]
        public int IdConfiguracionFlujo { get; set; }

        [Required]
        public int Orden { get; set ; }

        [Required]
        public int TipoRol { get; set ; }

        [Required]
        public bool EsFirma { get; set ; }

        [Required]
        public string Descripcion { get; set; }

        //[ForeignKey("IdRol")]
        //public Rol Rol { get; set; }

        [ForeignKey("IdConfiguracionFlujo")]
        public ConfiguracionFlujo Flujo { get; set; }

    }
}
