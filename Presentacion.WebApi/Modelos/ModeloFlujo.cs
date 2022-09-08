using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloFlujo<TPaso> : IFlujo<TPaso>
         where TPaso : IPaso
    {
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public List<TPaso> Pasos { get ;set ;}
        public int TipoFlujo { get; set; }
        public string Descripcion { get; set; }

        public bool IsValid()
        {
            if (this.Descripcion == null)
                return true;

            return false;
        }
    }
}
