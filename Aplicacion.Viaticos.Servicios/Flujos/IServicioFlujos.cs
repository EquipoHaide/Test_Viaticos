using System;
using Aplicacion.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios
{
    public interface IServicioFlujos : IServicioConfiguracionFlujoBase<Flujo, ConsultaConfiguracionFlujo, Paso>
    {
        void MetodoExtra();
    }
    
}
