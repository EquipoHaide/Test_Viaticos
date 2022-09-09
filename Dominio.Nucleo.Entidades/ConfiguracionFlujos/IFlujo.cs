using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public interface IFlujo : IEntity
    {        
        public int TipoFlujo { get; set; }
      
        public int IdTipoEntePublico { get; set; }
        
        public int IdNivelEmpleado { get; set; }


    }
}
