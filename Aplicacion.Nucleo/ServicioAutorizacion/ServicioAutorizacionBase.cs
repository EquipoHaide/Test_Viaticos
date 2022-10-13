using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TInstaciaCondensada,TAutorizacion, TQuery> : IServicioAutorizacionBase<TInstaciaCondensada,TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TInstaciaCondensada : class, IInstanciaCondensada
        where TQuery : class, IConsultaSolicitud
    {

        const string TAG = "Aplicacion.Nucleo.ServicioFlujoAutorizacion";

        public virtual Dominio.Nucleo.Servicios.ServicioAutorizacion.IServicioAutorizacionBase<TInstaciaCondensada,TAutorizacion> ServicioDominio { get; }

        public virtual IRepositorioAutorizacionBase<TInstaciaCondensada,TAutorizacion,TQuery> Repositorio { get; }

        //public virtual IRepositorioConfiguracionFlujoBase<TFlujo, TQuery> RepositorioConfiguracionFlkujo { get; }

        public Respuesta<ConsultaPaginada<TInstaciaCondensada>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<TInstaciaCondensada>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarAutorizaciones(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<TInstaciaCondensada>>(TAG);

            return new Respuesta<ConsultaPaginada<TInstaciaCondensada>>(recursos.Contenido);
        }

        public Respuesta AdministrarAutorizaciones(List<TInstaciaCondensada> Autorizacones, List<TFlujo> flujos, int Accion, string subjectId)
        {
            if (Autorizacones == null || Autorizacones.Count() <= 0)
                return new Respuesta("Es requerido una solicitud", TAG);

            if(Accion <= 0 )
                return new Respuesta("Es requerido alguna accion para la(s) solicitud(es)", TAG);

            var listaIds = Autorizacones.Select(r => r.IdAutorizacion).ToList();

            var ultimaAutorizacion = Repositorio.Try( r => r.ObtenerAutorizacion(listaIds));

            if (ultimaAutorizacion.EsError) return ultimaAutorizacion.ErrorBaseDatos(TAG);

            

            var respuesta = ServicioDominio.AdministrarAutorizacion(Autorizacones, subjectId);

            return new Respuesta();
        }

      
    }
}
