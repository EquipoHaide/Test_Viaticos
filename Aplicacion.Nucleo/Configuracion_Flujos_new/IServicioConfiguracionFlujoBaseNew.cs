using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;


namespace Aplicacion.Nucleo
{
    public interface IServicioConfiguracionFlujoBaseNew
    {
        Respuesta<ConsultaPaginada<IFlujoNew>> Consultar(IConsulta parametros, string subjectId);

        Respuesta<List<IFlujoNew>> Crear(List<IFlujoNew> flujos, string subjectId);

       
    }
}
