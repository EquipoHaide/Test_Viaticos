using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using DominioServicio = Dominio.Viaticos.Servicios;
namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo : ServicioConfiguracionFlujoBase<PasoViatico>, IServicioFlujo<PasoViatico>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<PasoViatico> servicio;
        DominioServicio.IServicioFlujos<PasoViatico> Servicio => App.Inject(ref servicio);

        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<PasoViatico> ServicioDominio => this.Servicio;


        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override bool ValidarPasos(IFlujo<PasoViatico> flujos)
        {

           

            return true; 
        }

       
    }
}
