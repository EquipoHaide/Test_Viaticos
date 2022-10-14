using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TFlujo,TPaso,TQuery> : IServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion, TFlujo, TPaso,TQuery>
        where TAutorizacion : class, IAutorizacion
        where TSolicitudCondensada : class, ISolicitudCondensada
        where TQuery : class, IQuery
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {

        const string TAG = "Aplicacion.Nucleo.ServicioFlujoAutorizacion";

        public virtual Dominio.Nucleo.Servicios.ServicioAutorizacion.IServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TFlujo,TPaso> ServicioDominio { get; }

        public virtual IRepositorioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TQuery> Repositorio { get; }

        public Respuesta<ConsultaPaginada<TSolicitudCondensada>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<TSolicitudCondensada>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarAutorizaciones(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<TSolicitudCondensada>>(TAG);

            return new Respuesta<ConsultaPaginada<TSolicitudCondensada>>(recursos.Contenido);
        }

        public Respuesta AdministrarAutorizaciones(List<TSolicitudCondensada> solicitudes, List<TFlujo> flujos, int accion, string subjectId)
        {
            if (solicitudes == null || solicitudes.Count() <= 0)
                return new Respuesta("Es requerido una solicitud", TAG);
            
            //Pendiente crear el enumerable de accion (devolver, devolver inicio, autorizar, etc)
            if(accion <= 0 )
                return new Respuesta("Es requerido alguna accion para la(s) solicitud(es)", TAG);

            var listaIds = solicitudes.Select(r => r.IdAutorizacion).ToList();

            var ultimaAutorizacion = Repositorio.Try( r => r.ObtenerAutorizacion(listaIds));

            if (ultimaAutorizacion.EsError) return ultimaAutorizacion.ErrorBaseDatos(TAG);

            var listaIdsFlujo = ultimaAutorizacion.Contenido.Select(r => r.IdFlujo).ToList();

            var idsAutorizacion = ultimaAutorizacion.Contenido.Select(r => r.IdFlujo).ToList();

            //var flujosCorrespondientes = RepositorioFlujo.Try(r => r.ObtenerFlujosPorAutorizacion(idsAutorizacion));


            //var respuesta = ServicioDominio.AdministrarAutorizacion(Autorizacones, subjectId);



            return new Respuesta();
        }

       
    }
}
