using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Nucleo.Firma;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public abstract class ServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TFlujo,TPaso,TQuery> : IServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion, TFlujo, TPaso,TQuery>
        where TAutorizacion : class, IAutorizacion
        where TSolicitudCondensada : class, ISolicitudCondensada
        where TQuery : class, IQuery
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {

        Nucleo.IAplicacion App { get; set; }

        const string TAG = "Aplicacion.Nucleo.ServicioFlujoAutorizacion";

        public IServicioFirmaElectronica servicioFirma;
        public IServicioFirmaElectronica ServicioFirma => this.App.Inject(ref servicioFirma);


        public virtual Dominio.Nucleo.Servicios.ServicioAutorizacion.IServicioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TFlujo,TPaso> ServicioDominio { get; }


        public virtual IRepositorioAutorizacionBase<TSolicitudCondensada,TAutorizacion,TQuery> Repositorio { get; }

        public abstract Respuesta AdministracionFinalAutorizacion (List<TSolicitudCondensada> solicitudes, List<TAutorizacion> autorizaciones, string subjectId);


        public ServicioAutorizacionBase(Nucleo.IAplicacion app)
        {
            App = app;
        }

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

            if(accion <= 0 )
                return new Respuesta("Es requerido alguna accion para la(s) solicitud(es)", TAG);

            var idsSolicitud = solicitudes.Select(r => r.Id).ToList();

            var listaSolicitudes = Repositorio.Try(r => r.ObtenerSolicitudesCondensadas(idsSolicitud));

            var listaIds = solicitudes.Select(r => r.IdAutorizacion).ToList();

            var listaAutorizaciones = Repositorio.Try(r => r.ObtenerAutorizacion(listaIds));

            if (listaAutorizaciones.EsError) return listaAutorizaciones.ErrorBaseDatos(TAG);

            var respuesta = ServicioDominio.AdministrarAutorizacion(solicitudes, listaSolicitudes.Contenido, listaAutorizaciones.Contenido, flujos, accion, subjectId);

            if (respuesta.EsExito)
            {
                var respuestaComplemantarioa = AdministracionFinalAutorizacion(listaSolicitudes.Contenido, listaAutorizaciones.Contenido, subjectId);

                if (respuestaComplemantarioa.EsExito)
                    return new Respuesta();
                else
                    return new Respuesta(respuestaComplemantarioa.Mensaje, respuestaComplemantarioa.TAG);
                
            }

            return new Respuesta(respuesta.Mensaje,respuesta.TAG);
        }

       
    }
}
