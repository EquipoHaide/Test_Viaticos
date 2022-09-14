using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioFlujos<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
        void Consultar(ConsultaConfiguracionFlujo parametros, string subjectId);

        void Crear(List<Flujo> flujos, string subjectId);

        void Eliminar(Flujo flujo, string subjectId);

        void Modificar(Flujo flujo, string subjectId);
    }
}
