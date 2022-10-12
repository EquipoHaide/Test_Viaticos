using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public interface IServicioAutorizacionViaticos<TAutorizacion, TQuery> : IServicioAutorizacionBase<TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TQuery : class, IConsultaSolicitud
    {
    }
}
