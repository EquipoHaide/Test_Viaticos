using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo
{
     public abstract class ServicioConfiguracionFlujoBase: IServicioConfiguracionFlujoBase
    {
      
        public abstract Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId);

        public abstract Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId);

        public abstract void Eliminar(IFlujo flujo, string subjectId);

        public abstract void Modificar(IFlujo flujo, string subjectId);

    }
}
