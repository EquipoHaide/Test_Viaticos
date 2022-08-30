using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Seguridad.Modelos;
using IdentityModel;
using Infraestructura.Transversal.Perfiles;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;

namespace Presentacion.WebApi.Controllers.Seguridad
{
    [Route("[controller]")]
    [Authorize]
    [Authorization]
    public class SesionesController : ControllerBase
    {
        IAplicacion App { get; set; }
        IServicioUsuarios servicio;
        IServicioUsuarios Servicio => App.Inject(ref servicio);

        public SesionesController(IAplicacion app)
        {
            App = app;
        }

        [HttpPost("")]
        public async Task<object> IniciarAsync()
        {
            try
            {
                var usuario = await DatosUsuarioAsync();
                var subjectId = this.GetSubjectId();
                usuario.EsCliente = subjectId == null;
                subjectId = usuario.EsCliente ? this.GetClientId() : subjectId;

                var resultado = await Servicio.IniciarSesion(usuario, this.GetAccesToken(), this.GetAccessTokenExpiration(), subjectId);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                //carga de perfiles
                //_ = App.GetPerfilManager().CargarPerfilesUsuarioAsync(subjectId, this.GetAccesToken());

                return this.ApiResult(resultado.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPut("{id:int}")]
        public dynamic ActualizarToken(int id)
        {
            try
            {
                var resultado = Servicio.ActualizarTokenSesion(id, this.GetAccesToken(), this.GetAccessTokenExpiration());

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

        [HttpDelete("{id:int}")]
        public dynamic Finalizar(int id)
        {
            try
            {
                var resultado = Servicio.CerrarSesion(id);

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

        [HttpPost("perfiles/{perfil}")]
        public dynamic EstablecerPrincipal(string perfil)
        {
            try
            {
                App.GetPerfilManager().EstablecerPrincipal(this.GetSubjectId(), perfil);

                return this.ApiResult();
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpGet("perfiles")]
        public dynamic ObtenerPerfiles()
        {
            try
            {
                List<PerfilUsuario> perfiles = App.GetPerfilManager().ObtenerPerfiles(this.GetSubjectId());

                return this.ApiResult(perfiles);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        private async Task<Usuario> DatosUsuarioAsync()
        {
            Usuario usuario = new Usuario();
            var claims = await this.GetUserInfoAsync();

            usuario.Nombre = claims.Find(c => c.Type == JwtClaimTypes.Name)?.Value;
            usuario.Apellidos = claims.Find(c => c.Type == JwtClaimTypes.FamilyName)?.Value;
            usuario.CorreoElectronicoPersonal = claims.Find(c => c.Type == JwtClaimTypes.Email)?.Value;
            usuario.CorreoElectronicoLaboral = claims.Find(c => c.Type == "inst_email")?.Value;
            usuario.TelefonoPersonal = claims.Find(c => c.Type == JwtClaimTypes.PhoneNumber)?.Value;
            usuario.TelefonoLaboral = claims.Find(c => c.Type == "area_phone_number")?.Value;
            usuario.NombreUsuario = claims.Find(c => c.Type == JwtClaimTypes.NickName)?.Value;
            usuario.NumeroEmpleado = claims.Find(c => c.Type == "employee_number")?.Value;
            usuario.AreaAdscripcion = claims.Find(c => c.Type == "area")?.Value;
            usuario.Dependencia = claims.Find(c => c.Type == "dependence")?.Value;
            usuario.Extension = claims.Find(c => c.Type == "area_extension")?.Value;

            return usuario;
        }
    }
}
