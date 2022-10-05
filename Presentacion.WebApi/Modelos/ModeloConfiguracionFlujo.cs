using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloConfiguracionFlujo<TFlujo,TPaso>
      where TFlujo : class, Dominio.Nucleo.Entidades.IFlujo<TPaso>
      where TPaso : Dominio.Nucleo.Entidades.IPaso
    {
        public List<TFlujo> Flujos { get; set; }
    }

   
}
