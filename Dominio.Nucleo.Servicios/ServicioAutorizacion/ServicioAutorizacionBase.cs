using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso> : IServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion,new()
        where TInstanciaCondensada : class, ISolicitudCondensada
    {

        const string TAG = "Dominio.Nucleo.Servicios.ServicioAutorizacion";

        public Respuesta AdministrarAutorizacion(List<TInstanciaCondensada> instanciaCondensadas, List<TInstanciaCondensada> instanciaCondensadasOriginales,
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
                var autorizacion = autorizaciones.Where(r => r.Id == solicitud.IdAutorizacion).FirstOrDefault();

                if(autorizacion == null)
                    return new Respuesta("No se encontro autorizacion relacionada con la solicitud", TAG);

                var flujo = flujos.Where(r => r.Id == autorizacion.IdFlujo).FirstOrDefault();

                if (flujo == null)
                    return new Respuesta("No se encontro flujo relacionada con la solicitud", TAG);

                if (flujo.Pasos == null || (flujo.Pasos.Where(p => p.Activo).Count() <= 0))
                    return new Respuesta("El flujo debe tener por lo menos un paso.", TAG);

                var pasosOrdenados = flujo.Pasos.OrderBy(r => r.Orden).ToArray();

                var pasoActual = pasosOrdenados.Where(r => r.Orden == solicitud.Orden).FirstOrDefault();

                var pasoSiguiente = pasosOrdenados.Where(r => r.Orden == solicitud.Orden + 1).FirstOrDefault();

                var pasoAnterior = pasosOrdenados.Where(r => r.Orden == solicitud.Orden - 1).FirstOrDefault();


                if ((int)AccionSolicitud.Autorizado == accion)
                {
                 
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion,AccionSolicitud.Autorizado,subjectId);

                    var nuevaAutorizacion = new TAutorizacion()
                    {
                        Orden = pasoSiguiente.Orden,
                        IdRol = pasoSiguiente.IdRolAutoriza,
                        Sello = autorizacion.Sello, ///PENDIENTE
                        Estado = (int)AccionSolicitud.Pendiente,
                        IdFlujo = autorizacion.IdFlujo,
              
                    };

                    autorizaciones.Add(nuevaAutorizacion);

                    //Actualizacion de la Solicitud (Instancia Condensada
                
                    solicitud.IdAutorizacion = nuevaAutorizacion.Id;
                    solicitud.Estado = (int)AccionSolicitud.Pendiente;
                    solicitud.Orden = pasoSiguiente.Orden;
                    solicitud.IdRol = pasoSiguiente.IdRolAutoriza;
                    

                }
                else if ((int)AccionSolicitud.Devuelto == accion)
                {
                    if (pasoActual.Orden - 1 == 0) {

                        //Actualizar autorizacion
                        ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.DevueltoInicio, subjectId);

                        //Actualizacion de la Solicitud (Instancia Condensada)
                        solicitud.Estado = (int)AccionSolicitud.Cancelado;
                        solicitud.FechaCancelacion = DateTime.Now;
                        solicitud.IdUsuarioCancelacion = subjectId;
                      
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
                        solicitud.IdAutorizacion = nuevaAutorizacion.Id;
                        solicitud.Estado = (int)AccionSolicitud.Pendiente;
                        solicitud.Orden = pasoAnterior.Orden;
                        solicitud.IdRol = pasoAnterior.IdRolAutoriza;

                    }

                }
                else if ((int)AccionSolicitud.DevueltoInicio == accion)
                {
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.DevueltoInicio, subjectId);

                    //Actualizacion de la Solicitud (Instancia Condensada)
                    solicitud.Estado = (int)AccionSolicitud.DevueltoInicio;
                    solicitud.FechaCancelacion = DateTime.Now;
                    solicitud.IdUsuarioCancelacion = subjectId;
                }
                else if ((int)AccionSolicitud.Cancelado == accion)
                {
                    //Actualizar autorizacion
                    ActualizarInstanciaActualizacion(autorizacion, AccionSolicitud.Cancelado, subjectId);

                    //Actualizacion de la Solicitud (Instancia Condensada)
                    solicitud.Estado = (int)AccionSolicitud.Cancelado;
                    solicitud.FechaCancelacion = DateTime.Now;
                    solicitud.IdUsuarioCancelacion = subjectId;
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
                autorizacion.IdUsuarioAutorizacion = subjectId;
                autorizacion.FechaAutorizacion = DateTime.Now;
          
            }else
            {
                autorizacion.IdUsuarioCancelacion = subjectId;
                autorizacion.FechaCancelacion = DateTime.Now;
            }
        }

     
    }
}
