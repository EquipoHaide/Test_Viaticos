using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo.Servicios;

namespace Aplicacion.Nucleo
{
    public abstract class ServicioConfiguracionFlujoBaseNew : IServicioConfiguracionFlujoBaseNew
    {
        public abstract Respuesta<ConsultaPaginada<IFlujoNew>> Consultar(IConsulta parametros, string subjectId);

        public Respuesta<List<IFlujoNew>> Crear(List<IFlujoNew> flujos, string subjectId)
        {
            this.ValidarFlujo(flujos);


            //De momento lo dejare asi
            return new Respuesta<List<IFlujoNew>>(flujos);
        }

        private Respuesta<bool> ValidarFlujo(List<IFlujoNew> flujos)
        {
            //invocar a Dominio base
            //...


            return new Respuesta<bool>(true);
        }
    }
}
