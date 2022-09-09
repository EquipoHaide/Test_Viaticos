﻿using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloFlujo<TPaso> : IFlujo<TPaso>
         where TPaso : IPaso
    {
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public List<TPaso> Pasos { get ;set ;}
        public int TipoFlujo { get; set; }
        public string Descripcion { get; set; }
        public string NombreFlujo { get; set; }
        public string OtroCampo { get; set; }
        public string NuevoCampo { get; set; }

        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(Descripcion) && Descripcion.Length > 100) return false;

            if (TipoFlujo < 0) return false;

            if (TipoEntePublico == null) return false;

            if (NivelEmpleado == null) return false;

            return true;
        }
    }
}
