using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public interface IFlujoNew
    {

        int TipoEntePublico { get; set; }

        double NivelEmpleado { get; set; }

        List<IPaso> Pasos { get; set; }

        int TipoFlujo { get; set; }

        bool IsValid();
    }
}
