using System;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioAutorizacionViaticos<TSolicitudCondesada,TAutorizacion, TFlujo, TPaso> : IServicioAutorizacionBase<TSolicitudCondesada,TAutorizacion,TFlujo,TPaso>
         where TFlujo : class, IFlujo<TPaso>
         where TPaso : class, IPaso
         where TAutorizacion : class,IAutorizacion  
         where TSolicitudCondesada : class, IInstanciaCondensada
    {
    }
}
