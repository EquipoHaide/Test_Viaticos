
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{

    /// <summary>
    /// Configuracion del Flujo 
    /// </summary>
    [Table("Flujo", Schema = "Viaticos")]
    public class ConfiguracionFlujo : IFlujo<PasoViatico>
    {
        public int Id { get; set; }

        [Required]
        public int IdNivelEmpleado { get; set; }
 
        [Required]
        public int IdEntePublico { get; set; }

        [Required]
        public int TipoFlujo { get; set; }

        /// Ejemplo de Variables añadidas por los desarrolladores 
        [Required]
        public string NombreFlujo { get; set; }
        [Required]
        public bool Activo { get; set; }
        ///

        public List<PasoViatico> Pasos { get; set; }

        public string DescripcionEntePublico { get; set; }

        public string Nivel { get; set; }


        [ForeignKey("idEntePublico")]
        public TipoEntePublico TipoEntePublico { get; set; }

        [ForeignKey("IdNivelEmpleado")]
        public NivelEmpleado NivelEmpleado { get; set; }

    


    }




}
