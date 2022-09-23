using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Viaticos.Entidades
{
    [Table("NivelEmpleado", Schema = "Viaticos")]
    public class NivelEmpleado : Dominio.Nucleo.INivelEmpleado
    {
        public int Id { get; set; }

        [Required]
        public string Nivel { get; set; }
    }
}
