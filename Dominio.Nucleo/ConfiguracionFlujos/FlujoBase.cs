
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{

    public abstract class FlujoBase : IFlujoNew
    {
        public ITipoEntePublico TipoEntePublico { get; set; }
        public INivelEmpleado NivelEmpleado { get; set; }
        public List<IPaso> Pasos { get; set; }

        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
    
}


