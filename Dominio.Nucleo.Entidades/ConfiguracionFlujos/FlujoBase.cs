using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public class FlujoBase : IFlujo
    {
        [Required]
        public int Id { get; set; }
        //public List<Paso> Pasos { get; set; }
        [Required]
        public int TipoFlujo { get; set; }
        [Required]
        public int IdTipoEntePublico { get; set; }
        
        //Solo cuando sea un flujo particular se requiere el nivel de empleado
        public int IdNivelEmpleado { get; set; }


       
    }
}
