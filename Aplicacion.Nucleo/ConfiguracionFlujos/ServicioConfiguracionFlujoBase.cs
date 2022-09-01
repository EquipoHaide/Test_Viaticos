using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo
{
    public abstract class ServicioConfiguracionFlujoBase<TFlujo, TConsulta,TPaso> : IServicioConfiguracionFlujoBase
    {
        public virtual Dominio.Nucleo.Servicios.IServicioConfiguracionFlujoBase<TFlujo, TPaso> ServicioDominio { get; }
        //public abstract Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId);

        //public abstract Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId);

        //public abstract void Eliminar(IFlujo flujo, string subjectId);

        //public abstract void Modificar(IFlujo flujo, string subjectId);
        public abstract Respuesta<ConsultaPaginada<TConsulta>> Consultar(TConsulta parametros, string subjectId);
      
        public abstract Respuesta<List<TFlujo>> Crear(List<IFlujo> flujos, string subjectId);
       
        public abstract void Eliminar(TFlujo flujo, string subjectId);

        public abstract void Modificar(TFlujo flujo, string subjectId);


        public Respuesta<bool> ValidarFlujo(List<TFlujo> flujos)
        {
            var respuesta = ServicioDominio.ValidarFlujo(flujos);

            return new Respuesta<bool>("");
        }

    }
}
