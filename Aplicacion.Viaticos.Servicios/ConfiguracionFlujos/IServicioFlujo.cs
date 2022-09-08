using System;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public interface IServicioFlujo<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
        where TPaso : IPaso
    {
    }
}
