using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;

namespace Presentacion.WebApi.Controllers.Seguridad
{
    [Route("[controller]")]
    [Authorize]
    [Authorization]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        IAplicacion App { get; set; }

        IServicioUsuarios servicio;
        IServicioUsuarios Servicio => App.Inject(ref servicio);

        public UsuariosController(IAplicacion app)
        {
            App = app;
        }

        [HttpGet(Name = "ConsultarUsuarios")]
        public object Consultar([FromQuery] ConsultarUsuariosModelo filtro)
        {
            try
            {
                var consulta = Servicio.ConsultarUsuarios(filtro);

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

        [HttpPut("{id:int}", Name = "ActivarUsuario")]
        public object ActivarUsuario(int id)
        {
            try
            {
                var resultado = Servicio.ActualizarEstado(true, id);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult();
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpDelete("{id:int}", Name = "DesactivarUsuario")]
        public object DesactivarUsuario(int id)
        {
            try
            {
                var resultado = Servicio.ActualizarEstado(true, id);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult();
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpDelete("roles", Name = "ConsultarRolesUsuario")]
        public object ConsultarRolesUsuario([FromBody] ConsultaRol filtro)
        {
            try
            {
                var consulta = Servicio.ConsultarRoles(filtro, this.GetSubjectId());

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

        [HttpPost("roles", Name = "AdministrarRolesUsuario")]
        public object AdministrarRoles([FromBody]List<RolDeUsuarioBase> roles)
        {
            try
            {
                var resultado = Servicio.AdministrarRoles(roles, this.GetSubjectId());

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(resultado.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpGet("{id:int}")]
        public object Obtener(int id)
        {
            try
            {
                var consulta = Servicio.ObtenerPerfil(id);

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

        [HttpGet("lista")]
        public object ObtenerUsuarios()
        {
            try
            {
                var consulta = Servicio.ObtenerUsuarios();

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

        [HttpGet("listaAutocomplete")]
        public object ObtenerUsuariosAutocomplete([FromQuery]ConsultarUsuariosModelo filtro)
        {
            try
            {
                var consulta = Servicio.ObtenerUsuariosAutocomplete(filtro);

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
    }
}
