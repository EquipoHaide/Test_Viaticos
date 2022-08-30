using Aplicacion.Nucleo;
using Infraestructura.Transversal.Perfiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Presentacion.WebApi.Seguridad
{
	public class AuthPerfilesFilter : ActionFilterAttribute
	{
		public IAplicacion App { get; set; }

		private bool EsEspecifico { get; set; }

		public AuthPerfilesFilter(bool esEspecifico = false)
        {
			EsEspecifico = esEspecifico;
			App = WebApi.App.Aplicacion.Instancia;
		}

		public override async void OnActionExecuting(ActionExecutingContext context)
		{
			var controller = context.Controller as ControllerBase;
			string subjectId = controller.GetSubjectId();
			string accessToken = controller.GetAccesToken();
			var perfilManager = App.GetPerfilManager();
			
			List<PerfilUsuario> perfiles = perfilManager.ObtenerPerfiles(subjectId);

			if(perfiles.Count == 0)
            {
				await perfilManager.CargarPerfilesUsuarioAsync(subjectId, accessToken);
            }

			var estadoPerfiles = await perfilManager.IntrospeccionPerfilAsync(subjectId, accessToken);			
			
			if(EsEspecifico)
            {
				PerfilUsuario perfilPrincipal = perfilManager.ObtenerPerfilPrincipal(subjectId);

				if (!estadoPerfiles.Any(ep => ep.SubjectId == perfilPrincipal.SubjectId && ep.Activo))
					PreconditionFailedResponse(context);
			}
			else
            {
				if (!estadoPerfiles.Any(ep => ep.Activo))
					PreconditionFailedResponse(context);
			}

			base.OnActionExecuting(context);
		}

		private void PreconditionFailedResponse(ActionExecutingContext context)
		{
			context.HttpContext.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
			context.HttpContext.Response.Headers.Clear();

			context.Result = new EmptyResult();
		}
	}
}