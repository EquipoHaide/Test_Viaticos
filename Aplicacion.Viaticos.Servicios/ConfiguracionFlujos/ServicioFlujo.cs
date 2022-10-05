using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Modelos;
using Entidades = Dominio.Nucleo.Entidades;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Transversal.Plataforma;
using DominioServicio = Dominio.Viaticos.Servicios;
using EntidadesViaticos = Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma.Extensiones;
using System.Linq;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo :
        ServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo> ,
        IServicioFlujo<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> servicio;
        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> Servicio => App.Inject(ref servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViatico;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViatico => App.Inject(ref repositorioConfiguracionFlujoViatico);

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViatico, ConsultaConfiguracionFlujo> Repositorio => this.RepositorioConfiguracionFlujoViatico;


        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<List<FlujoViatico>> CreacionFlujo(List<FlujoViatico> flujos, string subjectId)
        {

            List<FlujoViatico> flujosCreados = null;
            foreach (var flujo in flujos)
            {
                var respuesta = Servicio.Crear(flujo, false, subjectId);

                if (respuesta.EsError)
                    return new Respuesta<List<FlujoViatico>>(respuesta.Mensaje, respuesta.TAG);

                flujosCreados.Add(respuesta.Contenido);
            }

            return new Respuesta<List<FlujoViatico>>(flujosCreados);
        }

        public override Respuesta<List<FlujoViatico>> ModificarFlujo(List<FlujoViatico> flujos, List<FlujoViatico> flujosOriginales, string subjectId)
        {
            List<FlujoViatico> flujosModificados = null;

            foreach (var flujo in flujos)
            {
                var flujoOriginal = flujosOriginales.Where(r => r.Id == flujo.Id).FirstOrDefault();

                var respuesta = Servicio.Modificar(flujo, flujoOriginal, false, subjectId);

                if (respuesta.EsError)
                    return new Respuesta<List<FlujoViatico>>(respuesta.Mensaje, respuesta.TAG);

                flujosModificados.Add(respuesta.Contenido);
            }
      
            return new Respuesta<List<FlujoViatico>>(flujosModificados);
        }

        public override Respuesta<List<FlujoViatico>> EliminarFlujo(List<FlujoViatico> flujos, string subjectId)
        {
            //var totalFlujos = RepositorioConfiguracionFlujoViatico.ObtenerTotalFlujos(flujo.IdTipoEnte);

            //var esPredeterminado = RepositorioConfiguracionFlujoViatico.Try(r => r.ExisteFlujoPredeterminado(flujo));

            //if (esPredeterminado.EsError)
            //    return new Respuesta<List<ConfiguracionFlujo>>(esPredeterminado.Mensaje, TAG);

            List<FlujoViatico> flujosEliminados = null;

            foreach (var flujo in flujos)
            {
                var respuesta = Servicio.Eliminar(flujo, null, false, subjectId);

                if (respuesta.EsError)
                    return new Respuesta<List<FlujoViatico>>(respuesta.Mensaje, respuesta.TAG);

                flujosEliminados.Add(respuesta.Contenido);
            }
           

            return new Respuesta<List<FlujoViatico>>(flujosEliminados);
        }

        
    }
}
