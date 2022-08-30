using System.Net;
using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentacion.WebApi.Seguridad
{
	public class Authorization : ActionFilterAttribute
	{
		public IAplicacion App { get; set; }

		IServicioAcciones servicioAcciones;
		IServicioAcciones ServicioAcciones => App.Inject(ref servicioAcciones);

		public bool EsRecursoProtegido { get; set; }

		public string AccionConsultarRecursos { get; set; }

		public string AccionAdministrarRecursos { get; set; }

		public Authorization(bool esRecursoProtegido = false, string accionConsultarRecursos = "", string accionAdministrarRecursos = "")
		{
			App = WebApi.App.Aplicacion.Instancia;
			EsRecursoProtegido = esRecursoProtegido;
			AccionConsultarRecursos = accionConsultarRecursos;
			AccionAdministrarRecursos = accionAdministrarRecursos;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{

			if (WebApi.App.Aplicacion.Instancia.AuthorizationEnabled)
			{
				string nombreAccion = string.Empty;

				if (EsRecursoProtegido)
				{
					string accion = context.RouteData.Values["action"].ToString();

					if (accion == "ConsultarRecursos")
						nombreAccion = AccionConsultarRecursos;
					else if (accion == "AdministrarRecursos")
						nombreAccion = AccionAdministrarRecursos;
				}
				else
				{
					nombreAccion = context.ActionDescriptor.AttributeRouteInfo.Name;
				}

				if (!string.IsNullOrEmpty(nombreAccion))
				{
					if (!ServicioAcciones.Validar(nombreAccion, (context.Controller as ControllerBase).GetIdentityId()).Contenido)
						ForbiddenResponse(context);
				}
			}

			base.OnActionExecuting(context);
		}

		private void ForbiddenResponse(ActionExecutingContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
			context.HttpContext.Response.Headers.Clear();

			context.Result = new EmptyResult();
		}
	}
}