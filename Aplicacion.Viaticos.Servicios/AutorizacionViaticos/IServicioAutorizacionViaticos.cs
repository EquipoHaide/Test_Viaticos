using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public interface IServicioAutorizacionViaticos<TSolicitudCondensada,TAutorizacion, TFlujo, TPaso, TQuery> : IServicioAutorizacionBase<TSolicitudCondensada, TAutorizacion,TFlujo,TPaso, TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion
        where TSolicitudCondensada : class,IInstanciaCondensada
        where TQuery : class, IQuery
    {
    }
}
