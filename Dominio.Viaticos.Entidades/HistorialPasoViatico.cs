using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("HitorialesPasos", Schema = "Viaticos")]
    public class HistorialPasoViatico : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdPaso { get; set ; }

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


        [ForeignKey("IdPaso")]
        public PasoViatico PasoViatico { get; set; }

        [Required]
        public string IdUsuarioModifico { get; set; }
        [Required]
        public DateTime OperacionInicio { get; set; }
        [Required]
        public DateTime? OperacionFin { get; set; }

    }
}
