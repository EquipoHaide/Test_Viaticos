﻿using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class Flujo : IFlujo
    {
        public ITipoEntePublico TipoEntePublico { get; set; } 
        public INivelEmpleado NivelEmpleado     { get; set; }
        public List<IPaso> Pasos                { get; set; }
        public int TipoFlujo                    { get; set; }
        public string Descripcion               { get; set; }
        

        public bool IsValid()
        {
            return true;
        }
    }
}
