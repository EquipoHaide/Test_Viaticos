using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo.Servicios;

namespace Aplicacion.Nucleo
{
    public abstract class ServicioConfiguracionFlujoBase<TFlujo, TConsulta,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TConsulta,TPaso>
        where TFlujo : IFlujo
        where TConsulta : IConsulta
        where TPaso : IPaso
    {
        public virtual IServicioConfiguracionFlujoBase<TFlujo, TPaso> ServicioDominio { get; }

        //protected ServicioConfiguracionFlujoBase(IServicioConfiguracionFlujoBase<TFlujo, TPaso> servicioDominio)
        //{
        //    ServicioDominio = servicioDominio;
        //}

        public abstract Respuesta<ConsultaPaginada<TConsulta>> Consultar(TConsulta parametros, string subjectId);
      
        public abstract Respuesta<List<TFlujo>> Crear(List<TFlujo> flujos, string subjectId);
       
        public abstract void Eliminar(TFlujo flujo, string subjectId);

        public abstract void Modificar(TFlujo flujo, string subjectId);


        public Respuesta<bool> ValidarFlujo(List<TFlujo> flujos)
        {
            var respuesta = ServicioDominio.ValidarFlujo(flujos);

            return new Respuesta<bool>("");
        }

    }
}
