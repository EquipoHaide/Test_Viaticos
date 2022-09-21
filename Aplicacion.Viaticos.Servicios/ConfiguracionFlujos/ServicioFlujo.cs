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

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo : ServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> , IServicioFlujo<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> servicio;
        DominioServicio.IServicioFlujos<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> Servicio => App.Inject(ref servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);

        public override IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViaticos, EntidadesViaticos.PasoViatico> Repositorio => this.RepositorioConfiguracionFlujoViaticos;





        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<bool> ValidarPasos(EntidadesViaticos.FlujoViaticos flujos)
        {
            throw new NotImplementedException();
        }
    }
}
