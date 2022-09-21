using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloConfiguracionFlujo<TFlujo,TPaso>
      where TFlujo : class, IFlujo<TPaso>
      where TPaso : IPaso
    {
        public TFlujo Flujo { get; set; }
    }

   
}
