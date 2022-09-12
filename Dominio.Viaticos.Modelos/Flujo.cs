using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class Flujo : IFlujo<Paso>
    {
        public int Id { get; set; }

        public List<Paso> Pasos { get; set; }

        public int TipoFlujo { get; set; }

        public NivelEmpleado NivelEmpleado { get; set; }

        public TipoEntePublico TipoEntePublico { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
