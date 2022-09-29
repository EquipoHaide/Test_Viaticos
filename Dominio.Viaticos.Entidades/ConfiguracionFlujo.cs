
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
    public class ConfiguracionFlujo : Seguimiento, IFlujo<PasoViatico> 
    {
        public int Id { get; set; }

        
        public int IdNivelEmpleado { get; set; }
 
        [Required]
        public int IdEntePublico { get; set; }

        [Required]
        public int TipoFlujo { get; set; }

        /// Ejemplo de Variables añadidas por los desarrolladores 
        [Required]
        public string NombreFlujo { get; set; }

        [Required]
        public int Clasificacion { get; set; }
        ///

        public List<PasoViatico> Pasos { get; set; }


        [ForeignKey("IdEntePublico")]
        public TipoEntePublico EntePublico { get; set; }

        [ForeignKey("IdNivelEmpleado")]
        public NivelEmpleado Nivel { get; set; }
    }




}
