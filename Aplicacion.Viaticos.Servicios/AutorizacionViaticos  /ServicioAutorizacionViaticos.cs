using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Entidades = Dominio.Viaticos.Entidades;
using Modelos = Dominio.Viaticos.Modelos;
using DominioServicio = Dominio.Viaticos.Servicios;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.Autorizacion, Modelos.ConsultaSolicitudes>,
        IServicioAutorizacionViaticos<Entidades.Autorizacion, Modelos.ConsultaSolicitudes>
    {

        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }

        DominioServicio.IServicioAutorizacionViaticos<Entidades.Autorizacion> servicio;
        DominioServicio.IServicioAutorizacionViaticos<Entidades.Autorizacion> Servicio => App.Inject(ref servicio);

        //public override IServicioAutorizacionBase<Entidades.Autorizacion> ServicioDomino => this.Servicio; 


        //DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> servicio;
        //DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> Servicio => App.Inject(ref servicio);
        //public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViatico, EntidadesViaticos.PasoViatico> ServicioDominio => this.Servicio;


        public ServicioAutorizacionViaticos(Nucleo.IAplicacion app)
        {
            this.App = app;
        }
    }
}
