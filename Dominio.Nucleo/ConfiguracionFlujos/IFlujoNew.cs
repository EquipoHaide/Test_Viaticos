using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public interface IFlujoNew
    {

        TipoEntePublico TipoEntePublico { get; set; }
      
        //INivelEmpleado NivelEmpleado { get; set; }

        List<Paso> Pasos { get; set; }

        int TipoFlujo { get; set; }

        bool IsValid();


       
    }
}
