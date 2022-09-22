using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Entidades
{
    [Table("EntePublico", Schema = "Viaticos")]
    public class TipoEntePublico : ITipoEntePublico
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
