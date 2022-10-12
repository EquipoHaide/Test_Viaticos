using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Entidades = Dominio.Viaticos.Entidades;
using Modelos = Dominio.Viaticos.Modelos;
using DominioServicio = Dominio.Viaticos.Servicios;
using ServicioBase = Dominio.Nucleo.Servicios.ServicioAutorizacion;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;
using Dominio.Viaticos.Entidades;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.Autorizacion, Modelos.ConsultaSolicitudes>,
        IServicioAutorizacionViaticos<Entidades.Autorizacion, Modelos.ConsultaSolicitudes>
    {

        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }

        DominioServicio.IServicioAutorizacionViaticos<Entidades.Autorizacion> servicio;
        DominioServicio.IServicioAutorizacionViaticos<Entidades.Autorizacion> Servicio => App.Inject(ref servicio);
        public override ServicioBase.IServicioAutorizacionBase<Entidades.Autorizacion> ServicioDominio { get; }

        //IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViatico;
        //IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViatico => App.Inject(ref repositorioConfiguracionFlujoViatico);

        //public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViatico, ConsultaConfiguracionFlujo> Repositorio => this.RepositorioConfiguracionFlujoViatico;

        


        public ServicioAutorizacionViaticos(Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
