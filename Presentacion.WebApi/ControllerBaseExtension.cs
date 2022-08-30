using System;
using IdentityModel;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Seguimiento;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi
{
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// Serializa un objeto ApiResponse con success = true
        /// </summary>
        public static RespuestaApi<TContent> ApiResult<TContent>(this ControllerBase controllerBase, TContent content)
        {
            return new RespuestaApi<TContent>(content);
        }

        /// <summary>
        /// Serializa un objeto ApiResponse con success = true
        /// </summary>
        public static RespuestaApi ApiResult(this ControllerBase controllerBase)
        {
            return new RespuestaApi();
        }

        /// <summary>
        /// Serializa un objeto ApiResponse con success = false
        /// </summary>
        public static RespuestaApi ApiResult(this ControllerBase controllerBase, string message)
        {
            return new RespuestaApi(message);
        }

        /// <summary>
        /// Serializa un objeto ApiResponse con success = false y Fatal = true
        /// </summary>
        public static RespuestaApi ApiResult(this ControllerBase controllerBase, Exception exception, IApplicationLogger logger)
        {
            var id = CryptoRandom.CreateUniqueId(16);
            logger.Log.Fatal(exception, "Ha occurrido un error {id}", id);

            return new RespuestaApi($"Ha occurrido un error {id}");
        }
    }
}