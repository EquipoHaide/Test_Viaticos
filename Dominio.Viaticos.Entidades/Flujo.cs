
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Entidades
{
    public class Flujo : IFlujo<Paso>
    {
        public List<Paso> Pasos { get; set; }
        public int TipoFlujo { get; set; }
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
