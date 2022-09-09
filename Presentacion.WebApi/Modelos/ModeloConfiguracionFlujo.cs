using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloConfiguracionFlujo<TPaso>
        where TPaso:IPaso
    {
        public ModeloFlujo<TPaso> Flujo { get; set; }
    }
}
