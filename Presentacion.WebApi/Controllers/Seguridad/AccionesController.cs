using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Seguridad.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.RecursosProtegidos;
using Presentacion.WebApi.Seguridad;

namespace Presentacion.WebApi.Controllers.Seguridad
{
    [Route("[controller]")]
    [Authorize]
    [Authorization(true, "ConsultarRecursosAccion", "AdministrarRecursosAccion")]
    [ApiController]
    public class AccionesController : RecursoProtegidoController<RecursoDeAccion, ConsultaRecursoAccionModelo>
    {
        IServicioAcciones servicioAcciones;
        IServicioAcciones ServicioAcciones => App.Inject(ref servicioAcciones);

        public override IServicioRecursoBase ServicioRecursos => ServicioAcciones;

        public AccionesController(IAplicacion app)
        {
            this.App = app;
        }
    }
}
