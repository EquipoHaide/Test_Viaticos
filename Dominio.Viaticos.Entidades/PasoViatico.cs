using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("Pasos", Schema = "Viaticos")]
    public class PasoViatico : Seguimiento,  IPaso
    {
        public int Id { get; set ; }

        [Required]
        public int IdFlujo { get; set; }

        [Required]
        public int IdRolAutoriza { get; set; }

        [Required]
        public int TipoRol { get; set ; }

        [Required]
        public int Orden { get; set; }

        [Required]
        public bool AplicaFirma { get; set ; }


        [ForeignKey("IdFlujo")]
        public FlujoViatico Flujo { get; set; }
        
    }
}
