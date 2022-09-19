using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo.Entidades
{
    public class FlujoBase <TPaso> : IFlujo<TPaso>, IEntity
           where TPaso : IPaso
    {
        [Required]
        public int Id { get; set; }
      
        [Required]
        public int TipoFlujo { get; set; }
        [Required]
        public int IdTipoEntePublico { get; set; }


        public int IdNivelEmpleado { get; set; }

        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public List<TPaso> Pasos { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
