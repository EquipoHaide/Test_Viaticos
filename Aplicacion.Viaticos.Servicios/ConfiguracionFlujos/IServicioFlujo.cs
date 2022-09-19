using System;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public interface IServicioFlujo<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : Dominio.Nucleo.Entidades.FlujoBase<TPaso>//class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
       
    }
}
