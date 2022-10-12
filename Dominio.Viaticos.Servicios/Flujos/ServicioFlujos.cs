
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


            //creacion de los historiales si se requirieran, se inicio con su elaboracion a manera de ejemplo.
            foreach (var item in flujos)
            {
                var flujoOriginal = flujosOriginales.Find(f => f.Id == item.Id);
                if (item.IdNivelEmpleado != flujoOriginal.IdNivelEmpleado)
                {
                    //crear  el historial de flujo de acuerdo al flujo que se esta procesando...
                    var nuevoHistorial = new HistorialFlujoViatico();
                    nuevoHistorial.IdFlujo = flujoOriginal.Id;
                    nuevoHistorial.IdNivelEmpleado = flujoOriginal.IdNivelEmpleado;
                    nuevoHistorial.IdTipoEnte = flujoOriginal.IdTipoEnte;
                    //etc, terminar el seteo

                    item.Historiales.Add(nuevoHistorial);

                }

            }
            throw new NotImplementedException();
        }

       
    }
}
