using System;
using Presentacion.WebApi.ConfiguracionFlujo;
using Entidad = Dominio.Viaticos.Entidades;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api2/[controller]")]
    //[Authorize]
    public class PasosViaticosController : PasosBaseController<Entidad.PasoViatico>
    {
        AplicacionViaticos.IServicioPaso<Entidad.PasoViatico> servicioViaticos;
        AplicacionViaticos.IServicioPaso<Entidad.PasoViatico> ServicioViaticos => App.Inject(ref servicioViaticos);

        public override IServicioPasoBase<Entidad.PasoViatico> ServicioPasosBase => this.ServicioViaticos;


        public PasosViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
