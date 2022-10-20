using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TSolicitudCondensada, TAutorizacion, TFlujo, TPaso> : IServicioAutorizacionBase<TSolicitudCondensada, TAutorizacion, TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion,new()
        where TSolicitudCondensada : class, ISolicitudCondensada
    {

        const string TAG = "Dominio.Nucleo.Servicios.ServicioAutorizacion";

        public Respuesta AdministrarAutorizacion(List<TSolicitudCondensada> instanciaCondensadas, List<TSolicitudCondensada> instanciaCondensadasOriginales,
            List<TAutorizacion> autorizaciones, List<TFlujo> flujos, int accion, string subjectId)
        {
            if (autorizaciones == null || autorizaciones.Count() <= 0)
                    return new Respuesta("",TAG);

            if (flujos == null || flujos.Count() <= 0)
                return new Respuesta("", TAG);

            if (accion <= 0)
                return new Respuesta("Es necesario que especifique una Acción", TAG);


            foreach (var solicitud in instanciaCondensadas)
            {
                var solicitudOriginal = instanciaCondensadasOriginales.Where(r => r.Id == solicitud.Id).FirstOrDefault();

                if (solicitudOriginal == null)
                    return new Respuesta("No se encontro la solicitud", TAG);

                if (solicitudOriginal.Estado != (int)AccionSolicitud.Pendiente)
                    return new Respuesta("La solicitud ya fue previamente afectada", TAG);

                var autorizacion = autorizaciones.Where(r => r.Id == solicitudOriginal.IdAutorizacion).FirstOrDefault();

                if(autorizacion == null)
                    return new Respuesta("No se encontro autorizacion relacionada con la solicitud", TAG);

                var flujo = flujos.Where(r => r.Id == autorizacion.IdFlujo).FirstOrDefault();

                if (flujo == null)
                    return new Respuesta("No se encontro flujo relacionada con la solicitud", TAG);

                if (flujo.Pasos == null || (flujo.Pasos.Where(p => p.Activo).Count() <= 0))
                    return new Respuesta("El flujo debe tener por lo menos un paso.", TAG);

                var pasosOrdenados = flujo.Pasos.OrderBy(r => r.Orden).ToArray();

                var pasoActual = pasosOrdenados.Where(r => r.Orden == solicitudOriginal.Orden).FirstOrDefault();

                var pasoSiguiente = pasosOrdenados.Where(r => r.Orden == solicitudOriginal.Orden + 1).FirstOrDefault();

                var pasoAnterior = pasosOrdenados.Where(r => r.Orden == solicitudOriginal.Orden - 1).FirstOrDefault();

                var pasoFinal = pasosOrdenados.LastOrDefault(); 


                if ((int)AccionSolicitud.Autorizado == accion)
                {
                 
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion,AccionSolicitud.Autorizado,subjectId);

                    TAutorizacion nuevaAutorizacion = new TAutorizacion();

                    if (pasoSiguiente != null)
                    {
                        nuevaAutorizacion.Orden = pasoSiguiente.Orden;
                        nuevaAutorizacion.IdRol = pasoSiguiente.IdRolAutoriza;
                        nuevaAutorizacion.Sello = autorizacion.Sello; ///PENDIENTE
                        nuevaAutorizacion.Estado = (int)AccionSolicitud.Pendiente;
                        nuevaAutorizacion.IdFlujo = autorizacion.IdFlujo;

                        autorizaciones.Add(nuevaAutorizacion);

                        //Actualizacion de la Solicitud (Instancia Condensada)
                        solicitudOriginal.IdAutorizacion = nuevaAutorizacion.Id;
                        solicitudOriginal.Estado = (int)AccionSolicitud.Pendiente;
                        solicitudOriginal.Orden = pasoSiguiente.Orden;
                        solicitudOriginal.IdRol = pasoSiguiente.IdRolAutoriza;

                    } else if(pasoFinal.Orden == solicitudOriginal.Orden)
                    {
                        //Actualizacion de la Solicitud (Instancia Condensada)
                        solicitudOriginal.Estado = (int)AccionSolicitud.Autorizado;
                        solicitudOriginal.FechaAutorizacion = DateTime.Now;
                        solicitudOriginal.IdUsuarioAutorizacion = subjectId;
                    }

                }
                else if ((int)AccionSolicitud.Devuelto == accion)
                {
                    if (pasoActual.Orden - 1 == 0) {

                        //Actualizar autorizacion
                        ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.DevueltoInicio, subjectId);

                        //Actualizacion de la Solicitud (Instancia Condensada)
                        solicitudOriginal.Estado = (int)AccionSolicitud.Cancelado;
                        solicitudOriginal.FechaCancelacion = DateTime.Now;
                        solicitudOriginal.IdUsuarioCancelacion = subjectId;
                      
                    }
                    else{
                        //Actualizar autorizacion
                        ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.Devuelto, subjectId);

                        var nuevaAutorizacion = new TAutorizacion()
                        {
                            Orden = pasoAnterior.Orden,
                            IdRol = pasoAnterior.IdRolAutoriza,
                            Sello = autorizacion.Sello, ///PENDIENTE
                            Estado = (int)AccionSolicitud.Pendiente,
                            IdFlujo = autorizacion.IdFlujo,
                      
                        };

                        autorizaciones.Add(nuevaAutorizacion);

                        //Actualizacion de la Solicitud (Instancia Condensada)
                        solicitudOriginal.IdAutorizacion = nuevaAutorizacion.Id;
                        solicitudOriginal.Estado = (int)AccionSolicitud.Pendiente;
                        solicitudOriginal.Orden = pasoAnterior.Orden;
                        solicitudOriginal.IdRol = pasoAnterior.IdRolAutoriza;

                    }

                }
                else if ((int)AccionSolicitud.DevueltoInicio == accion)
                {
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.DevueltoInicio, subjectId);

                    //Actualizacion de la Solicitud (Instancia Condensada)
                    solicitudOriginal.Estado = (int)AccionSolicitud.DevueltoInicio;
                    solicitudOriginal.FechaCancelacion = DateTime.Now;
                    solicitudOriginal.IdUsuarioCancelacion = subjectId;
                }
                else if ((int)AccionSolicitud.Cancelado == accion)
                {
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.Cancelado, subjectId);

                    //Actualizacion de la Solicitud (Instancia Condensada)
                    solicitudOriginal.Estado = (int)AccionSolicitud.Cancelado;
                    solicitudOriginal.FechaCancelacion = DateTime.Now;
                    solicitudOriginal.IdUsuarioCancelacion = subjectId;
                }

            }


            return new Respuesta();
        }




        private Respuesta validarRecursosFirma(List<TSolicitudCondensada> solicitudesCondensadas) {

            foreach (var item in solicitudesCondensadas)
            {
                if (item.AplicaFirma) {
                    if (item.RecursosFirma?.Certificado?.Archivo is null || item.RecursosFirma?.Llave?.Archivo is null)
                        return new Respuesta("Ha ocurrido un error inesperado", TAG);

                    if (item.RecursosFirma.Certificado.Extension.IsNullOrEmptyOrWhiteSpace())
                        return new Respuesta("La extension es obligatoria", TAG);

                    if (item.RecursosFirma.Certificado.Archivo is FileStream fileStream && Path.GetExtension(fileStream.Name) != ".cer")
                        return new Respuesta("", TAG);

                    if (item.RecursosFirma.Certificado.Extension != ".cer")
                        return new Respuesta("", TAG);

                    if (item.RecursosFirma.Contrasena.IsNullOrEmptyOrWhiteSpace())
                        return new Respuesta("", TAG);

                    if (item.RecursosFirma?.Llave?.Archivo is null)
                        return new Respuesta("", TAG);

                    if (item.RecursosFirma.Llave.Extension.IsNullOrEmptyOrWhiteSpace())
                        return new Respuesta("La extension es obligatoria", TAG);

                    if (item.RecursosFirma.Llave.Extension != ".key")
                        return new Respuesta("Solo se permite seleccionar en el campo Key un archivo con extension Key", TAG);

                   
                }
            }
            return new Respuesta();
        }

       

        private void ActualizarInstanciaActualizacion (TAutorizacion autorizacion,AccionSolicitud accion,string subjectId)
        {
            //Actualizar autorizacion
            autorizacion.Estado = (int)accion;
           
 
            if(accion != AccionSolicitud.Autorizado)
            {
                autorizacion.IdUsuarioCancelacion = subjectId;
                autorizacion.FechaCancelacion = DateTime.Now;
            }
            else
            {
                autorizacion.IdUsuarioAutorizacion = subjectId;
                autorizacion.FechaAutorizacion = DateTime.Now;
            }
        }

     
    }
}
