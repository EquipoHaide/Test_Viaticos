using System;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioAutorizacionViaticos<TSolicitudCondesada,TAutorizacion> : IServicioAutorizacionBase<TSolicitudCondesada,TAutorizacion>
         where TAutorizacion : class,IAutorizacion  
         where TSolicitudCondesada : class, IInstanciaCondensada
    {
    }
}
