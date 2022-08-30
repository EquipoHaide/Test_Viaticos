using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;

namespace Presentacion.WebApi.Controllers.Seguridad
{
    [Route("[controller]")]
    [Authorize]
    [Authorization]
    [ApiController]
    public class AccesosController : ControllerBase
    {
        IAplicacion App { get; set; }
        IServicioAcciones servicioAcciones;
        public IServicioAcciones ServicioAcciones => App.Inject(ref servicioAcciones);

        public AccesosController(IAplicacion app)
        {
            App = app;
        }

        [HttpGet(Name = "ConsultarAccesos")]
        public object Consultar([FromQuery] ConsultaAcceso filtro)
        {
            try
            {
                var consulta = ServicioAcciones.ConsultarAccesos(filtro, this.GetSubjectId());

                if (consulta.EsError)
                {
                    if (consulta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(consulta.Mensaje);
                }

                return this.ApiResult(consulta.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPost(Name = "AdministrarAccesos")]
        public object Administrar([FromBody] List<Acceso> accesos)
        {
            try
            {
                var respuesta = ServicioAcciones.AdministrarAccesos(accesos, this.GetSubjectId());

                if (respuesta.EsError)
                {
                    if (respuesta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(respuesta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(respuesta.Mensaje);
                }

                return this.ApiResult(respuesta.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpGet("modulos")]
        public object ObtenerModulos()
        {
            try
            {
                var modulos = ServicioAcciones.ObtenerModulos(this.GetSubjectId());

                if (modulos.EsError)
                {
                    if (modulos.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(modulos.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(modulos.Mensaje);
                }

                return this.ApiResult(modulos.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPut]
        public object ValidarAccesos(List<string> acciones)
        {
            try
            {
                var accesos = ServicioAcciones.Validar(acciones, this.GetSubjectId());

                if (accesos.EsError)
                {
                    if (accesos.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(accesos.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(accesos.Mensaje);
                }

                return this.ApiResult(accesos.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }
    }
}