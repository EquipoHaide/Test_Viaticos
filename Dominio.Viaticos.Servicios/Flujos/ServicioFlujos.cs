
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

        public Respuesta<FlujoViatico> Crear(FlujoViatico flujo, bool validacionExtra, string subjectId)
        {

            var controlFlujo = new FlujoViatico() {
                IdTipoEnte = flujo.IdTipoEnte,
                IdNivelEmpleado = flujo.IdNivelEmpleado,
                Activo = flujo.Activo,
                    
            };

            foreach (var item in flujo.Pasos)
            {
                item.Seguir(subjectId);
            }

            controlFlujo.Pasos = flujo.Pasos;

            controlFlujo.Seguir(subjectId);

            return new Respuesta<FlujoViatico>(controlFlujo);
        }

        public Respuesta<FlujoViatico> Modificar(FlujoViatico flujo, FlujoViatico flujoOriginal, bool validacionExtra, string subjectId)
        {

            foreach (var item in flujoOriginal.Pasos)
            {
                if (item.Id == 0)
                    item.Seguir(subjectId);
                    flujoOriginal.Pasos.Add(item);

                if (item.Id > 0 && !item.Activo)
                    item.Seguir(subjectId, true);

                if (item.Id > 0)
                {
                    var itemOriginal = flujoOriginal.Pasos.FirstOrDefault(r => r.Id == item.Id);

                    itemOriginal.Orden = item.Orden;

                    itemOriginal.Seguir(subjectId, true, false);
                }
            }

            flujoOriginal.Seguir(subjectId, true, false);

            return new Respuesta<FlujoViatico>(flujoOriginal);
        }

        public Respuesta<FlujoViatico> Eliminar(FlujoViatico flujoOriginal, List<FlujoViatico> listaFlujos, bool esPredeterminado, string subjectId)
        {

            if (listaFlujos.Count() == 1)
                return new Respuesta<FlujoViatico>("",TAG);

            foreach (var item in flujoOriginal.Pasos)
            {
                if (item.Id > 0 && item.Activo)
                    item.Seguir(subjectId, true);
            }

            flujoOriginal.Seguir(subjectId, true);


            return new Respuesta<FlujoViatico>(flujoOriginal);
        }
     
    }
}
