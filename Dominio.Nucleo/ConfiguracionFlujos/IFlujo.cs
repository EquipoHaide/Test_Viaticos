﻿using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public interface IFlujo
    {

        ITipoEntePublico TipoEntePublico { get; set; }

        INivelEmpleado NivelEmpleado { get; set; }

        List<IPaso> Pasos { get; set; }

        int TipoFlujo { get; set; }

        bool IsValid();
    }
}
