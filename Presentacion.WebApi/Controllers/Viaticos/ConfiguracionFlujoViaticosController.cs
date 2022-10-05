using Dominio.Viaticos.Modelos;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.ConfiguracionFlujo                                                                                              ;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;
using Entidad = Dominio.Viaticos.Entidades;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ConfiguracionFlujoViaticosController :
        ConfiguracionFlujoAutorizacionBaseController<Entidad.FlujoViatico, Entidad.PasoViatico, ConsultaConfiguracionFlujo>
    {

        AplicacionViaticos.IServicioFlujo<Entidad.FlujoViatico, Entidad.PasoViatico, ConsultaConfiguracionFlujo> servicioViaticos;
        AplicacionViaticos.IServicioFlujo<Entidad.FlujoViatico, Entidad.PasoViatico, ConsultaConfiguracionFlujo> ServicioViaticos => App.Inject(ref servicioViaticos);

        public override IServicioConfiguracionFlujoBase<Entidad.FlujoViatico  , Entidad.PasoViatico, ConsultaConfiguracionFlujo> ServicioConfiguracionFlujoBase => this.ServicioViaticos;


        public ConfiguracionFlujoViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }

        
    }
}