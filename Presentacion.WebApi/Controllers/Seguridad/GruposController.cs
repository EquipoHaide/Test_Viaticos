using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.RecursosProtegidos;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.Controllers.Seguridad
{
    [Route("[controller]")]
    [Authorize]
    [Authorization(true, "ConsultarRecursosGrupo", "AdministrarRecursosGrupo")]
    [ApiController]
    public class GruposController : RecursoProtegidoController<RecursoDeGrupo, ConsultaRecursoGrupoModelo>
    {
        IServicioGrupos servicioGrupos;
        IServicioGrupos ServicioGrupos => App.Inject(ref servicioGrupos);

        IServicioAcciones servicioAcciones;
        public IServicioAcciones ServicioAcciones => App.Inject(ref servicioAcciones);

        public override IServicioRecursoBase ServicioRecursos => ServicioGrupos;

        public GruposController(IAplicacion app)
        {
            this.App = app;
        }

        [HttpGet(Name = "ConsultarGrupos")]
        public object Consultar([FromQuery] ConsultaGrupo filtro)
        {
            try
            {
                var consulta = ServicioGrupos.Consultar(filtro, this.GetSubjectId());

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

        [HttpPost(Name = "CrearGrupo")]
        public object Crear([FromBody] Grupo grupo)
        {
            try
            {
                var resultado = ServicioGrupos.Crear(grupo, this.GetSubjectId());

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado.Contenido.Id });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPut(Name = "ModificarGrupo")]
        public object Modificar([FromBody] Grupo grupo)
        {
            try
            {
                var resultado = ServicioGrupos.Modificar(grupo, this.GetSubjectId());

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

        [HttpDelete(Name = "EliminarGrupo")]
        public object Eliminar([FromQuery] string cadenaIds)
        {
            try
            {
                int intValue = 0;
                List<int> idsGrupos = cadenaIds.Split(",").ToList().Select(id => int.TryParse(id, out intValue) ? int.Parse(id) : 0).ToList();
                idsGrupos.RemoveAll(id => id == 0);

                if (!idsGrupos.Any())
                    return this.ApiResult("Bad Request");

                var grupos = idsGrupos.Select(id => new Grupo { Id = id }).ToList();

                var resultado = ServicioGrupos.Eliminar(grupos, this.GetSubjectId());

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

        [HttpGet("usuarios", Name = "ConsultarUsuariosGrupo")]
        public object ConsultarUsuariosGrupo([FromQuery] ConsultaUsuariosGrupo filtro)
        {
            try
            {
                var consulta = ServicioGrupos.ConsultarUsuarios(filtro, this.GetSubjectId());

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

        [HttpPut("usuarios", Name = "AdministrarUsuariosGrupo")]
        public object AdministrarUsuarios([FromBody] List<UsuarioDeGrupoBase> usuarios)
        {
            try
            {
                var resultado = ServicioGrupos.AdministrarUsuarios(usuarios, this.GetSubjectId());

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

        [HttpGet("roles", Name = "ConsultarRolesGrupo")]
        public object ConsultarRolesGrupo([FromQuery] ConsultaRolGrupo filtro)
        {
            try
            {
                var consulta = ServicioGrupos.ConsultarRoles(filtro, this.GetSubjectId());

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

        [HttpPut("usuarios", Name = "AdministrarRolesGrupo")]
        public object AdministrarRoles([FromBody] List<RolDeGrupoBase> roles)
        {
            try
            {
                var resultado = ServicioGrupos.AdministrarRoles(roles, this.GetSubjectId());

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
                var consulta = ServicioGrupos.ObtenerPorLectura(id, this.GetSubjectId());

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
