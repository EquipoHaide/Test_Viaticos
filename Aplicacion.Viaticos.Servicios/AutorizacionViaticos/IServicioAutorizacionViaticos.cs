using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public interface IServicioAutorizacionViaticos<TSolicitudCondensada,TAutorizacion, TQuery> : IServicioAutorizacionBase<TSolicitudCondensada, TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TSolicitudCondensada : class,IInstanciaCondensada
        where TQuery : class, IConsultaSolicitud
    {
    }
}
