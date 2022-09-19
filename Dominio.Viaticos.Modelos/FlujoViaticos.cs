using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class FlujoViaticos : IFlujo<PasoViatico> 
    {
        public int Id { get; set; }
        public string NombreFlujo { get; set; }
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public List<PasoViatico> Pasos { get; set; }

        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            return true;
        }





    }
}
