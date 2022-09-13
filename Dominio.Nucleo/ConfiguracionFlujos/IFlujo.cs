using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public interface IFlujo<TPaso> : IModel
        where TPaso : IPaso
    {

        int Id { get; set; }

        TipoEntePublico TipoEntePublico { get; set; }

        NivelEmpleado NivelEmpleado { get; set; }

        List<TPaso> Pasos { get; set; }

        int TipoFlujo { get; set; }

    }
}
