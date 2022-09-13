using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Viaticos.Modelos
{
    public class ModeloFlujo<TPaso> : IFlujo<TPaso> , IModel
         where TPaso : IPaso
    {
        public int Id {get; set;}
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public List<TPaso> Pasos { get ;set ;}
        public int TipoFlujo { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Activo { get; set; }


        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(Descripcion) && Descripcion.Length > 100) return false;

            if (TipoFlujo < 0) return false;

            

            return true;
        }
    }
}
