﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public interface IFlujo<TPaso> : IEntity
       where TPaso : IPaso
    {

        //TipoEntePublico TipoEntePublico { get; set; }
        public int IdEntePublico { get; set; }
        public string DescripcionEntePublico { get; set; }

       //NivelEmpleado NivelEmpleado { get; set; }
        public int IdNivelEmpleado { get; set; }
        public string Nivel { get; set; }

        List<TPaso> Pasos { get; set; }

        /// <summary>
        /// Es un Enumerable
        /// 1 --> Predeterminado
        /// 2 --> Particular
        /// </summary>
        int TipoFlujo { get; set; }

    }
}
