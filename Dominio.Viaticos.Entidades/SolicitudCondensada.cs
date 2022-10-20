using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("SolicitudesCondensadas", Schema = "Viaticos")]
    public class SolicitudCondensada : SeguimientoCreacion, ISolicitudCondensada
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdAutorizacion { get; set; }
        [Required]
        public string Folio { get; set; }

        public string Concepto { get; set; }

        [Required]
        public int Estado { get; set; }

        [Required]
        public int Orden { get; set; }
        [Required]
        public int IdRol { get; set; }

        public string IdUsuarioAutorizacion { get; set; }

        public DateTime? FechaAutorizacion { get; set; }

        public string IdUsuarioCancelacion { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public DateTime FechaAfectacion { get; set; }

        [Required]
        public bool AplicaFirma { get; set; }


    }
}
