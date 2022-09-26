using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Entidades
{
    [Table("EntePublicos", Schema = "Viaticos")]
    public class TipoEntePublico : Dominio.Nucleo.ITipoEntePublico
    {
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
