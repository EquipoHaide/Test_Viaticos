using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public abstract class FlujoBase 
    {
        public int TipoEntePublico { get; set; }
        public double NivelEmpleado { get; set; }
        public List<IPaso> Pasos { get; set; }
        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
