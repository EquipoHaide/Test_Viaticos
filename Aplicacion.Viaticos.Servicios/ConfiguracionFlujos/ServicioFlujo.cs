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
        ServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo> ,
        IServicioFlujo<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> servicio;
        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> Servicio => App.Inject(ref servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico, ConsultaConfiguracionFlujo> Repositorio => this.RepositorioConfiguracionFlujoViaticos;


        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<FlujoViaticos> CreacionFlujo(FlujoViaticos flujo, string subjectId)
        {
            var respuesta = Servicio.Crear(flujo, false, subjectId);

            if(respuesta.EsError)
                return new Respuesta<FlujoViaticos>(respuesta.Mensaje,respuesta.TAG);


            return new Respuesta<FlujoViaticos>(flujo);
        }

        public override Respuesta<FlujoViaticos> ModificarFlujo(FlujoViaticos flujo, string subjectId)
        {
            var respuesta = Servicio.Modificar(flujo, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<FlujoViaticos>(respuesta.Mensaje, respuesta.TAG);


            return new Respuesta<FlujoViaticos>(flujo);
        }

        public override Respuesta<FlujoViaticos> EliminarFlujo(FlujoViaticos flujo, string subjectId)
        {
            var respuesta = Servicio.Eliminar(flujo, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<FlujoViaticos>(respuesta.Mensaje, respuesta.TAG);


            return new Respuesta<FlujoViaticos>(flujo);
        }
    }
}
