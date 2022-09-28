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

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo :
        ServicioConfiguracionFlujoBase<EntidadesViaticos.ConfiguracionFlujo, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo> ,
        IServicioFlujo<EntidadesViaticos.ConfiguracionFlujo, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<EntidadesViaticos.ConfiguracionFlujo, EntidadesViaticos.PasoViatico> servicio;
        DominioServicio.IServicioFlujos<EntidadesViaticos.ConfiguracionFlujo, EntidadesViaticos.PasoViatico> Servicio => App.Inject(ref servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<EntidadesViaticos.ConfiguracionFlujo, EntidadesViaticos.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViatico;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViatico => App.Inject(ref repositorioConfiguracionFlujoViatico);

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.ConfiguracionFlujo, ConsultaConfiguracionFlujo> Repositorio => this.RepositorioConfiguracionFlujoViatico;


        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<ConfiguracionFlujo> CreacionFlujo(ConfiguracionFlujo flujo, string subjectId)
        {
            var respuesta = Servicio.Crear(flujo, false, subjectId);

            if(respuesta.EsError)
                return new Respuesta<ConfiguracionFlujo>(respuesta.Mensaje,respuesta.TAG);


            return new Respuesta<ConfiguracionFlujo>(respuesta.Contenido);
        }

        public override Respuesta<ConfiguracionFlujo> ModificarFlujo(ConfiguracionFlujo flujo, ConfiguracionFlujo flujoOriginal, string subjectId)
        {
            var respuesta = Servicio.Modificar(flujo, flujoOriginal, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<ConfiguracionFlujo>(respuesta.Mensaje, respuesta.TAG);




            return new Respuesta<ConfiguracionFlujo>(respuesta.Contenido);
        }

        public override Respuesta<ConfiguracionFlujo> EliminarFlujo(ConfiguracionFlujo flujo, string subjectId)
        {
            var respuesta = Servicio.Eliminar(flujo, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<ConfiguracionFlujo>(respuesta.Mensaje, respuesta.TAG);


       


            return new Respuesta<ConfiguracionFlujo>(flujo);
        }
    }
}
