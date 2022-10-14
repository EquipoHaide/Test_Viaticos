using Microsoft.AspNetCore.Mvc;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Modelos;
using Presentacion.WebApi.AutorizacionSolicitudes;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios;
using AplicacionBase = Aplicacion.Nucleo.ServicioAutorizacion;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    public class SolicitudViaticosController : AutorizacionSolicitudBaseController<SolicitudCondensada, Autorizacion, FlujoViatico,PasoViatico,ConsultaSolicitudes>
    {

        AplicacionViaticos.AutorizacionViaticos.IServicioAutorizacionViaticos<SolicitudCondensada, Autorizacion, FlujoViatico, PasoViatico, ConsultaSolicitudes> servicioAutorizacionViaticos;
        AplicacionViaticos.AutorizacionViaticos.IServicioAutorizacionViaticos<SolicitudCondensada, Autorizacion, FlujoViatico, PasoViatico, ConsultaSolicitudes> ServicioAutorizacionViaticos => App.Inject(ref servicioAutorizacionViaticos);

        public override AplicacionBase.IServicioAutorizacionBase<SolicitudCondensada, Autorizacion, FlujoViatico, PasoViatico, ConsultaSolicitudes> ServicioAutorizacion => this.ServicioAutorizacionViaticos;

        public SolicitudViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
