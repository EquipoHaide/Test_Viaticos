
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{

    /// <summary>
    /// Configuracion del Flujo 
    /// </summary>
    [Table("Flujos", Schema = "Viaticos")]
    public class FlujoViatico : Seguimiento, IFlujo<PasoViatico> 
    {
        public int Id { get; set; }

        [Required]
        public int IdTipoEnte { get; set; }


        public int IdNivelEmpleado { get; set; }

        [Required]
        public int TipoFlujo { get; set; }


        public List<PasoViatico> Pasos { get; set; }

        [ForeignKey("IdTipoEnte")]
        public TipoEntePublico EntePublico { get; set; }

    }




}
