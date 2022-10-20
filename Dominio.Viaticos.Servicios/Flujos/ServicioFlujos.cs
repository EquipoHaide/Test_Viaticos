
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico>,
        IServicioFlujos<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public Respuesta<List<FlujoViatico>> CompletarAdministracionFlujos(List<FlujoViatico> flujos, List<FlujoViatico> flujosOriginales, string subjectId)
        {

            //agregar la logica de validacion de reglas de negocio propia de viaticos

            //PENDIENTE COMPLETAR la creacion de los historiales de los flujos y de los pasos si se requirieran, se inicio con su elaboracion a manera de ejemplo.            
            foreach (var flujoNuevo in flujos)
            {
                var flujoOriginal = flujosOriginales.Find(f => f.Id == flujoNuevo.Id);
                if (flujoNuevo.IdNivelEmpleado != flujoOriginal.IdNivelEmpleado)
                {
                    //crear  el historial de flujo de acuerdo al flujo que se esta procesando...
                    var historialFlujoViatico = new HistorialFlujoViatico();
                    historialFlujoViatico.IdFlujo = flujoOriginal.Id;
                    historialFlujoViatico.IdNivelEmpleado = flujoOriginal.IdNivelEmpleado;
                    historialFlujoViatico.IdTipoEnte = flujoOriginal.IdTipoEnte;
                    historialFlujoViatico.TipoFlujo = flujoOriginal.TipoFlujo;

                    historialFlujoViatico.IdUsuarioModifico = flujoOriginal.IdUsuarioModifico;
                    historialFlujoViatico.OperacionInicio = flujoOriginal.FechaModificacion;
                    //Si ya se aplico el seguimiento al flujo nuevo, puedo restarle un segundo, pero verificar
                    historialFlujoViatico.OperacionFin = flujoNuevo.FechaModificacion.AddSeconds(-1); 
       

                    flujoNuevo.Historiales.Add(historialFlujoViatico);

                }

                foreach (var paso in flujoNuevo.Pasos)
                {
                    var pasosOriginales = flujoOriginal.Pasos;
                    var pasoOriginal = pasosOriginales.Find(p=>p.Id == paso.Id);

                    if (paso.AplicaFirma != pasoOriginal.AplicaFirma ||
                        paso.IdRolAutoriza != pasoOriginal.IdRolAutoriza ||
                        paso.Orden != pasoOriginal.Orden ||
                        paso.TipoRol != pasoOriginal.TipoRol
                        ) 
                    {
                        var historialPaso = new HistorialPasoViatico();

                        historialPaso.IdPaso = pasoOriginal.Id;
                        historialPaso.IdFlujo = pasoOriginal.IdFlujo;
                        
                        historialPaso.IdRolAutoriza = pasoOriginal.IdRolAutoriza;
                        historialPaso.Orden = pasoOriginal.Orden;
                        historialPaso.TipoRol = pasoOriginal.TipoRol;
                        historialPaso.AplicaFirma = pasoOriginal.AplicaFirma;

                        historialPaso.IdUsuarioModifico = pasoOriginal.IdUsuarioModifico;
                        historialPaso.OperacionInicio = pasoOriginal.FechaModificacion;
                        //Si ya se aplico el seguimiento al paso viatico, puedo restarle un segundo, pero verificar
                        historialPaso.OperacionFin = paso.FechaModificacion.AddSeconds(-1);

                    }
                }

            }
            throw new NotImplementedException();
        }

        public override Respuesta<ResumenInformacion> ValidacioConfiguracionFlujos(List<FlujoViatico> flujos, string subjectId)
        {

            return new Respuesta<ResumenInformacion>("");
        }
    }
}
