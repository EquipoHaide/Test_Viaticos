using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class FlujoTemp 
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
