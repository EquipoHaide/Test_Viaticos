using System;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioAutorizacionViaticos<TAutorizacion> : IServicioAutorizacionBase<TAutorizacion>
         where TAutorizacion : class, IAutorizacion
    {
    }
}
