using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public interface IFlujo<T> where T : IPaso
    {

        //ITipoEntePublico TipoEntePublico { get; set; }

        //INivelEmpleado NivelEmpleado { get; set; }

        List<T> Pasos { get; set; }

        int TipoFlujo { get; set; }

        bool IsValid();
    }
}
