
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{

    public class FlujoBase : IFlujoNew
    {
        public TipoEntePublico TipoEntePublico { get; set; }
        //public INivelEmpleado NivelEmpleado { get; set; }
        public List<Paso> Pasos { get; set; }
        public FlujoBase() {

            //this.TipoEntePublico = new TipoEntePublico();
        }

        public int TipoFlujo { get; set; }
        //public ITipoEntePublico TipoEntePublico { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
    
}


