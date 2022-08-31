using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ConfiguracionFlujos
{
    public class ServicioConfiguracionFlujoBase : IServicioConfiguracionFlujoBase
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujoBase";

        public Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId)
        {
            throw new NotImplementedException();
        }

        public Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }


    }
}
