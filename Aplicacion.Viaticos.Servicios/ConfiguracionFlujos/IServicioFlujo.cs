using System;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public interface IServicioFlujo<TFlujo,TPaso,TQuery> : IServicioConfiguracionFlujoBase<TFlujo,TPaso,TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class , IConsultaFlujo
    {
       
    }
}
