using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    [Table("Paso", Schema = "Viaticos")]
    public class PasoViatico : IPaso
    {
        public int Id { get; set ; }

        [Required]
        public int IdRol { get; set; }

        [Required]
        public int Orden { get; set ; }

        [Required]
        public int Rol { get; set ; }

        [Required]
        public int TipoRol { get; set ; }

        [Required]
        public bool EsFirma { get; set ; }

    }
}
