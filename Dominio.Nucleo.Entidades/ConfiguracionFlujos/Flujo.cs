using System;
using System.Collections.Generic;

namespace Dominio.Nucleo.Entidades
{
    public class Flujo : IFlujo<Paso>
    {
        public ITipoEntePublico TipoEntePublico { get; set ; }
        public INivelEmpleado NivelEmpleado { get; set; }
        public List<Paso> Pasos { get; set; }
        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
