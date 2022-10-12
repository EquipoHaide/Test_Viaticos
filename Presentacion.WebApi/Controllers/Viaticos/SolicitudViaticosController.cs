using Microsoft.AspNetCore.Mvc;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Modelos;
using Presentacion.WebApi.AutorizacionSolicitudes;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios;
using AplicacionBase = Aplicacion.Nucleo.ServicioAutorizacion;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    public class SolicitudViaticosController : AutorizacionSolicitudBaseController<Autorizacion, ConsultaSolicitudes>
    {

        AplicacionViaticos.AutorizacionViaticos.IServicioAutorizacionViaticos<Autorizacion, ConsultaSolicitudes> servicioAutorizacionViaticos;
        AplicacionViaticos.AutorizacionViaticos.IServicioAutorizacionViaticos<Autorizacion, ConsultaSolicitudes> ServicioAutorizacionViaticos => App.Inject(ref servicioAutorizacionViaticos);

        public override AplicacionBase.IServicioAutorizacionBase<Autorizacion, ConsultaSolicitudes> ServicioAutorizacion => this.ServicioAutorizacionViaticos;

        public SolicitudViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
