
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{

    public abstract class FlujoBase
    {
        public int TipoEntePublico { get; set; }
        public double NivelEmpleado { get; set; }

        public List<IPaso>Pasos { get; set; }

        public int TipoFlujo { get; set; }



    }
    
}


