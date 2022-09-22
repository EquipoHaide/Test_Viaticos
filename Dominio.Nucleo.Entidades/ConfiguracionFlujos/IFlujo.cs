using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public interface IFlujo<TPaso> : IEntity
       where TPaso : IPaso
    {

        TipoEntePublico TipoEntePublico { get; set; }

        NivelEmpleado NivelEmpleado { get; set; }

        List<TPaso> Pasos { get; set; }

        int TipoFlujo { get; set; }

    }
}
