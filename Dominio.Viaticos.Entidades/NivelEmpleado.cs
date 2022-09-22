using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Viaticos.Entidades
{
    [Table("EntePublico", Schema = "Viaticos")]
    public class NivelEmpleado
    {
        public int Id { get; set; }

        [Required]
        public string Nivel { get; set; }
    }
}
