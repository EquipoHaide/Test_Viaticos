
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase<Dominio.Viaticos.Entidades.ConfiguracionFlujo, Dominio.Viaticos.Entidades.PasoViatico>,
        IServicioFlujos<Dominio.Viaticos.Entidades.ConfiguracionFlujo, Dominio.Viaticos.Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public Respuesta<ConfiguracionFlujo> Crear(ConfiguracionFlujo flujo, bool validacionExtra, string subjectId)
        {
            if (flujo.NombreFlujo == null)
                return new Respuesta<ConfiguracionFlujo>("Es necesario una descripcion en el flujo", TAG);


            var controlFlujo = new ConfiguracionFlujo() {
                IdEntePublico = flujo.IdEntePublico,
                IdNivelEmpleado = flujo.IdNivelEmpleado,
                TipoFlujo = flujo.TipoFlujo,
                NombreFlujo = flujo.NombreFlujo,
                Activo = flujo.Activo,
                Pasos = flujo.Pasos        
            };

            foreach (var item in flujo.Pasos)
            {
                item.Seguir(subjectId);
                
            }
            controlFlujo.Pasos = flujo.Pasos;

            controlFlujo.Seguir(subjectId);

            return new Respuesta<ConfiguracionFlujo>(controlFlujo);
        }

        public Respuesta<ConfiguracionFlujo> Eliminar(ConfiguracionFlujo flujoOriginal, List<ConfiguracionFlujo> listaFlujos, bool esPredeterminado, string subjectId)
        {

            if (listaFlujos.Count() == 1)
                return new Respuesta<ConfiguracionFlujo>("",TAG);

            foreach (var item in flujoOriginal.Pasos)
            {
                if (item.Id > 0 && item.Activo)
                    item.Seguir(subjectId, true);
            }

            flujoOriginal.Seguir(subjectId, true);


            return new Respuesta<ConfiguracionFlujo>(flujoOriginal);
        }

        public Respuesta<ConfiguracionFlujo> Modificar(ConfiguracionFlujo flujo, ConfiguracionFlujo flujoOriginal, bool validacionExtra, string subjectId)
        {

            flujoOriginal.IdNivelEmpleado = flujo.IdNivelEmpleado;
            flujoOriginal.IdEntePublico = flujo.IdEntePublico;
            flujoOriginal.Clasificacion = 1;
            flujoOriginal.NombreFlujo = flujo.NombreFlujo;
      

            foreach (var item in flujoOriginal.Pasos)
            {
                    if (item.Id > 0 && !item.Activo)
                        item.Seguir(subjectId, true,false);

            }

            flujoOriginal.Seguir(subjectId, true, false);

            return new Respuesta<ConfiguracionFlujo>(flujoOriginal);

        }
    }
}
