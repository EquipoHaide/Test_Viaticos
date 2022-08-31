using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ConfiguracionFlujos
{
    public interface IServicioConfiguracionFlujoBase
    {
        Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId);

        Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId);

        void Eliminar(IFlujo flujo, string subjectId);


        void Modificar(IFlujo flujo, string subjectId);
    }
}
