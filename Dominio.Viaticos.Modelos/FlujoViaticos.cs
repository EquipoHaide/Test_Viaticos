using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class FlujoViaticos //: FlujoBase
    {
        public TipoEntePublico TipoEntePublico { get; set; }
        //public INivelEmpleado NivelEmpleado { get; set; }
        public List<PasoViatico> Pasos { get; set; }

        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            return true;
        }



        public string NombreFlujo { get; set; }
        

       
    }
}
