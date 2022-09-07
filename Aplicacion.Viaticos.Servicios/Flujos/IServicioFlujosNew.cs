using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios
{
    public interface IServicioFlujosNew<T> : IServicioConfiguracionFlujoBaseNew
    {
        //void CrearViaticos(List<IFlujoNew> flujos);

        void CrearViaticos(List<T> flujos);
    }
    
}
