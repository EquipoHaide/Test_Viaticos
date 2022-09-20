
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{

    public class FlujoBase<TPaso> : IFlujo<TPaso>
        where TPaso : IPaso
    {   
        public TipoEntePublico TipoEntePublico { get; set; }
        //public INivelEmpleado NivelEmpleado { get; set; }
        public List<TPaso> Pasos { get; set; }
        public int Id { get; set ; }
        public NivelEmpleado NivelEmpleado { get; set ; }
        public int TipoFlujo { get; set ; }


        public bool IsValid()
        {
            return true;
        }
    }
    
}


