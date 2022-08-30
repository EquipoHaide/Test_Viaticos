using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguridad.Servicios.Usuarios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        const string TAG = "Dominio.Seguridad.Servicios.Usuarios.ServicioUsuarios";

        /// <summary>
        /// Inicia una sesión si el usuario esta habilitado y no ha superado el maximo de sesiones permitidas.
        /// </summary>
        public Respuesta<Sesion> IniciarSesion(Modelos.Usuario usuario, ref Entidades.Usuario usuarioLocal, string token, DateTime expirationToken)
        {
            if (token.IsNullOrEmptyOrWhiteSpace()) return new Respuesta<Sesion>(R.strings.TokenInvalido, TAG);
            if (expirationToken <= DateTime.Now) return new Respuesta<Sesion>(R.strings.TokenExpirado, TAG);

            var mensaje = "";
            if (usuarioLocal == null)
            {
                usuarioLocal = new Entidades.Usuario();
                usuario.Sincronizar(usuarioLocal);
                usuarioLocal.EsCliente = usuario.EsCliente;
                usuarioLocal.EsHabilitado = true;
                mensaje = R.strings.UsuarioNoExiste;
            }

            if (!usuarioLocal.EsHabilitado) return new Respuesta<Sesion>(R.strings.UsuarioDeshabilitado, TAG);
            if (usuarioLocal.Sesiones != null && usuarioLocal.Sesiones.Count >= usuarioLocal.SesionesPermitidas) return new Respuesta<Sesion>(R.strings.SesionesPermitidasSuperadas, TAG);

            var sesion = new Sesion()
            {
                IdUsuario = usuarioLocal.Id,
                Inicio = DateTime.Now,
                Token = token,
                Expira = expirationToken,
                TokenCount = 1
            };

            return new Respuesta<Sesion>(sesion) { Mensaje = mensaje };
        }
        /// <summary>
        /// Actualiza la informacion de una sesión
        /// </summary>
        public Respuesta<Sesion> ActualizarSesion(Sesion sesion, string token, DateTime expirationToken)
        {
            if (sesion == null) return new Respuesta<Sesion>(R.strings.SesionInvalida, TAG);
            if (token.IsNullOrEmptyOrWhiteSpace()) return new Respuesta<Sesion>(R.strings.TokenInvalido, TAG);
            if (expirationToken <= DateTime.Now) return new Respuesta<Sesion>(R.strings.TokenExpirado, TAG);

            sesion.Token = token;
            sesion.Expira = expirationToken;
            sesion.TokenCount++;
            return new Respuesta<Sesion>(sesion);
        }
        /// <summary>
        /// Permite cerrar una sesión expirando la vigencia de su token.
        /// </summary>
        public Respuesta<Sesion> CerrarSesion(Sesion sesion)
        {
            if (sesion == null) return new Respuesta<Sesion>(R.strings.SesionInvalida, TAG);

            sesion.Expira = DateTime.Now;
            return new Respuesta<Sesion>(sesion);
        }
        /// <summary>
        /// Actualiza el estado de un usuario Habilita/Deshabilita
        /// </summary>
        public Respuesta<bool> ActualizarEstado(Entidades.Usuario usuario, bool esHabilitado)
        {
            if (usuario == null) return new Respuesta<bool>(R.strings.UsuarioNoExiste, TAG);

            if (usuario.EsHabilitado != esHabilitado)
            {
                usuario.EsHabilitado = esHabilitado;
                return new Respuesta<bool>(true);
            }
            return new Respuesta<bool>(false);
        }
        /// <summary>
        /// Actualiza la cantidad de sesiones permitidas para un usuario.
        /// </summary>
        public Respuesta<bool> ActualizarCantidadDeSesionesPermitidas(Entidades.Usuario usuario, int sesionesPermitidas)
        {
            if (usuario == null) return new Respuesta<bool>(R.strings.UsuarioNoExiste, TAG);
            if (sesionesPermitidas >= 2) return new Respuesta<bool>(R.strings.SesionesPermitidasInvalidas, TAG);

            if (usuario.SesionesPermitidas != sesionesPermitidas)
            {
                usuario.SesionesPermitidas = sesionesPermitidas;
                return new Respuesta<bool>(true);
            }
            return new Respuesta<bool>(false);
        }
        /// <summary>
        /// Permite administrar el agregado o eliminado de roles al usuario.
        /// </summary>
        public Respuesta<List<RolUsuario>> AdministrarRoles(List<RolDeUsuarioBase> roles, IEnumerable<IPermiso> permisos, string subjectId)
        {
            if (roles == null || roles.Count <= 0) return new Respuesta<List<RolUsuario>>(new List<RolUsuario>());

            List<RolUsuario> result = new List<RolUsuario>();
            foreach (var rol in roles)
            {
                if (!permisos.Where(p => p.IdRecurso == rol.IdRol).TieneEjecucion())
                {
                    return new Respuesta<List<RolUsuario>>(R.strings.UsuarioNoTienePermisos, TAG);
                }

                if ((rol.Id > 0 && !rol.EsAsignado) || (rol.Id <= 0 && rol.EsAsignado))
                {
                    RolUsuario rolItem = new RolUsuario()
                    {
                        Id = rol.Id,
                        IdRol = rol.IdRol,
                        IdUsuario = rol.IdUsuario
                    };

                    if (rolItem.Id <= 0)
                    {
                        rolItem.Seguir(subjectId);
                    }

                    result.Add(rolItem);
                }
            }

            return new Respuesta<List<RolUsuario>>(result);
        }

        /// <summary>
        /// Método que valida un usuario según el subjectId dado.
        /// </summary>
        public Respuesta<Entidades.Usuario> ObtenerUsuario(Entidades.Usuario usuario)
        {
            if (usuario is null)
                return new Respuesta<Entidades.Usuario>(R.strings.UsuarioNoExiste, TAG);

            return new Respuesta<Entidades.Usuario>(usuario);
        }
    }
}
