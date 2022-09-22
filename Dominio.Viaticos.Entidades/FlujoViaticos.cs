
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo;
using Entidades = Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{

    /// <summary>
    /// Configuracion del Flujo 
    /// </summary>
    [Table("Flujo", Schema = "Viaticos")]
    public class FlujoViatico : IFlujo<PasoViatico>, IEntity   
    {
        public int Id { get; set; }

        [Required]
        public int idNivelEmpleado { get; set; }

        [Required]
        public int idEntePublico { get; set; }

        //A manera de ejemplo voy agrerar el campo nombre
        [Required]
        public string NombreFlujo { get; set; }

        [Required]
        public int TipoFlujo { get; set; }

        [ForeignKey("idEntePublico")]
        public TipoEntePublico TipoEntePublico { get; set; }

        [ForeignKey("idNivelEmpleado")]
        public NivelEmpleado NivelEmpleado { get; set; }


        public List<PasoViatico> Pasos { get; set; }

        Nucleo.TipoEntePublico IFlujo<PasoViatico>.TipoEntePublico { get; set; }
        Nucleo.NivelEmpleado IFlujo<PasoViatico>.NivelEmpleado { get; set; }
    }




}
