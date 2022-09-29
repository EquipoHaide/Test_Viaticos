using System;
using Dominio.Nucleo.Entidades;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public interface IServicioPaso<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {
   
    }
}
