using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("Autorizaciones", Schema = "Viaticos")]
    public class Autorizacion :Seguimiento,  IAutorizacion
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdFlujo { get; set ; }
        [Required]
        public int Orden { get ; set; }
        [Required]
        public int IdRol { get ; set; }

        public string Sello { get ; set; }
        [Required]
        public int Estado { get ; set; }

        public string IdUsuarioAutorizacion { get; set; }

        public DateTime? FechaAutorizacion { get; set; }

        public string IdUsuarioCancelacion { get; set; }

        public DateTime? FechaCancelacion { get; set; }
       

        /*
        public string IdUsuarioCreo { get; set; }

        public DateTime FechaCreacion { get; set; }

       */

    }
}
