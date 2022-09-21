
using System.Collections.Generic;
using Dominio.Nucleo;
using Entidades = Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    public class FlujoViaticos : IFlujo<PasoViatico>, IEntity
        
    {
        //A manera de ejemplo voy agrerar el campo nombre
        public string NombreFlujo { get; set; }
        public int Id { get; set; }
        public List<PasoViatico> Pasos { get ; set ; }
       
        public TipoEntePublico TipoEntePublico { get; set; }
        public NivelEmpleado NivelEmpleado { get; set; }
        public int TipoFlujo { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
