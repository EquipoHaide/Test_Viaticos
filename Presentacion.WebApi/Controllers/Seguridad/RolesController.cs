using Aplicacion.Nucleo;
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
    [Authorization(true, "ConsultarRecursosRol", "AdministrarRecursosRol")]
    [ApiController]
    public class RolesController : RecursoProtegidoController<RecursoDeRol, ConsultaRecursoRolModelo>
    {
        Aplicacion.Seguridad.Servicios.Core.IServicioRoles servicioRoles;
        Aplicacion.Seguridad.Servicios.Core.IServicioRoles ServicioRoles => App.Inject(ref servicioRoles);

        public override IServicioRecursoBase ServicioRecursos => ServicioRoles;
       
        public RolesController(IAplicacion app)
        {
            this.App = app;
        }


        [HttpGet(Name = "ConsultarRoles")]
        public object Consultar([FromQuery] ConsultaRol filtro)
        {
            try
            {
                var consulta = ServicioRoles.Consultar(filtro, this.GetSubjectId());

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

        [HttpPost(Name = "CrearRol")]
        public object Crear([FromBody] Rol rol)
        {
            try
            {
                var resultado = ServicioRoles.Crear(rol, this.GetSubjectId());

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

        [HttpPut(Name = "ModificarRol")]
        public object Modificar([FromBody] Rol rol)
        {
            try
            {
                var resultado = ServicioRoles.Modificar(rol, this.GetSubjectId());

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

        [HttpDelete(Name = "EliminarRol")]
        public object Eliminar([FromQuery] string cadenaIds)
        {
            try
            {
                int intValue = 0;
                List<int> idsRoles = cadenaIds.Split(",").ToList().Select(id => int.TryParse(id, out intValue) ? int.Parse(id) : 0).ToList();
                idsRoles.RemoveAll(id => id == 0);

                if (!idsRoles.Any())
                    return this.ApiResult("Bad Request");

                var roles = idsRoles.Select(id => new Rol { Id = id }).ToList();
                var resultado = ServicioRoles.Eliminar(roles, this.GetSubjectId());

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

        [HttpGet("{id:int}")]
        public object Obtener(int id)
        {
            try
            {
                var consulta = ServicioRoles.ObtenerPorLectura(id, this.GetSubjectId());

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