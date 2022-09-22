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

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo> Repositorio => this.RepositorioConfiguracionFlujoViatico;


        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<FlujoViatico> CreacionFlujo(FlujoViatico flujo, string subjectId)
        {
            var respuesta = Servicio.Crear(flujo, false, subjectId);

            if(respuesta.EsError)
                return new Respuesta<FlujoViatico>(respuesta.Mensaje,respuesta.TAG);


            return new Respuesta<FlujoViatico>(flujo);
        }

        public override Respuesta<FlujoViatico> ModificarFlujo(FlujoViatico flujo, string subjectId)
        {
            var respuesta = Servicio.Modificar(flujo, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<FlujoViatico>(respuesta.Mensaje, respuesta.TAG);


            return new Respuesta<FlujoViatico>(flujo);
        }

        public override Respuesta<FlujoViatico> EliminarFlujo(FlujoViatico flujo, string subjectId)
        {
            var respuesta = Servicio.Eliminar(flujo, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<FlujoViatico>(respuesta.Mensaje, respuesta.TAG);


            return new Respuesta<FlujoViatico>(flujo);
        }
    }
}
