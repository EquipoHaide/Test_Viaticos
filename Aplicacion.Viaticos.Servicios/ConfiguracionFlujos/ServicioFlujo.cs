using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Repositorios;
using DominioServicio = Dominio.Viaticos.Servicios;
using EntidadesViaticos = Dominio.Viaticos.Entidades;
namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo : ServicioConfiguracionFlujoBase<PasoViatico, EntidadesViaticos.FlujoViaticos>, IServicioFlujo<PasoViatico>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<PasoViatico> servicio;
        DominioServicio.IServicioFlujos<PasoViatico> Servicio => App.Inject(ref servicio);

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);

        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<PasoViatico> ServicioDominio => this.Servicio;

        //Verificar por que me pidio una conversion explicita.
        //public override IRepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> Repositorio => (IRepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase>)this.RepositorioConfiguracionFlujoViaticos;

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViaticos> Repositorio => RepositorioConfiguracionFlujoViaticos;




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
