using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public interface IFlujo<TPaso> : IEntity ,ISeguimiento
       where TPaso : IPaso
    {

        //TipoEntePublico TipoEntePublico { get; set; }
        public int IdTipoEnte { get; set; }
       
        //NivelEmpleado NivelEmpleado { get; set; }
        public int IdNivelEmpleado { get; set; }
        
        List<TPaso> Pasos { get; set; }

        /// <summary>
        /// Es un Enumerable
        /// 1 --> Predeterminado
        /// 2 --> Particular
        /// </summary>
        int TipoFlujo { get; set; }

    }
}
