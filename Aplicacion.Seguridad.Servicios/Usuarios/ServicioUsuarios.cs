using Dominio.Nucleo;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Modelos = Dominio.Seguridad.Modelos;

namespace Aplicacion.Seguridad.Servicios
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        const string TAG = "Aplicacion.Seguridad.Servicios.Usuarios.ServicioUsuarios";

        Nucleo.IAplicacion App { get; set; }

        Dominio.Seguridad.Servicios.Usuarios.IServicioUsuarios servicioDominio;
        Dominio.Seguridad.Servicios.Usuarios.IServicioUsuarios Servicio => App.Inject(ref servicioDominio);

        IRepositorioUsuarios repositorio;
        IRepositorioUsuarios Repositorio => App.Inject(ref repositorio);

        Core.IServicioRoles servicioRoles;
        Core.IServicioRoles ServicioRoles => App.Inject(ref servicioRoles);

        public ServicioUsuarios(Nucleo.IAplicacion app)
        {
            App = app;
        }

        /// <summary>
        /// Permite iniciar sesion si el usuario lo tiene permitido.
        /// </summary>
        public async Task<Respuesta<int>> IniciarSesion(Modelos.Usuario usuario, string token, DateTime expirationToken, string subjectId)
        {
            if (usuario == null) return new Respuesta<int>(R.strings.UsuarioNoProporcionado, TAG);
            //if (token.IsNotNullOrEmptyOrWhiteSpace()) return new Respuesta<int>(R.strings.TokenInvalido);
            if (token.IsNullOrEmptyOrWhiteSpace()) return new Respuesta<int>(R.strings.TokenInvalido, TAG);

            var usuarioLocal = Repositorio.Try(r => r.ObtenerUsuarioConSesiones(subjectId));
            if (usuarioLocal.EsError) return usuarioLocal.ErrorBaseDatos<int>(TAG);
            var usuarioLocalContenido = usuarioLocal.Contenido;
            var respuesta = Servicio.IniciarSesion(usuario, ref usuarioLocalContenido, token, expirationToken);
            if (respuesta.EsExito)
            {
                var esNuevo = false;
                try
                {
                    Task<int> respuestaRepositorio = null;
                    if (respuesta.Mensaje == Dominio.Seguridad.Servicios.R.strings.UsuarioNoExiste)
                    {
                        esNuevo = true;
                        usuarioLocalContenido.Sesiones = new List<Sesion>() { respuesta.Contenido };
                        usuarioLocalContenido.SubjectId = subjectId;
                        usuarioLocalContenido.FechaCreacion = DateTime.Now;
                        usuarioLocalContenido.FechaModificacion = DateTime.Now;
                        Repositorio.Add(usuarioLocalContenido);
                        respuestaRepositorio = Repositorio.SaveAsync();
                    }
                    else
                    {
                        Repositorio.AgregarSesion(respuesta.Contenido);
                        respuestaRepositorio = Repositorio.SaveAsync();
                    }

                    if (!esNuevo)
                    {
                        _ = Task.Run(() =>
                        {
                            if (usuario.Sincronizar(usuarioLocalContenido))
                            {
                                usuarioLocalContenido.FechaModificacion = DateTime.Now;
                                var repositorioTemp = App.Resolve<IRepositorioUsuarios>();
                                repositorioTemp.Update(usuarioLocalContenido);
                                var save = repositorioTemp.Try(r => r.Save());
                                if (save.EsError)
                                {
                                    Console.WriteLine(save.ExcepcionInterna.Message);//Sustituir esto por una entrada al log que indique que el ultimo cambio del usuario fue descartado por una conexion fallida con la base de datos.
                                }
                            }
                        });
                    }
                    var idSesion = await respuestaRepositorio;
                    return new Respuesta<int>(respuesta.Contenido.Id);
                }
                catch (Exception ex)
                {
                    return new Respuesta<int>(ex, R.strings.ErrorConexionBaseDeDatos, TAG);
                }
            }
            else
            {
                return new Respuesta<int>(-1, EstadoProceso.Fallido) { Mensaje = respuesta.Mensaje, TAG = TAG };
            }
        }
        /// <summary>
        /// Permite actualizar el token de una sesión.
        /// </summary>
        public Respuesta ActualizarTokenSesion(int idSesion, string token, DateTime expirationToken)
        {
            var sesion = Repositorio.Try(r => r.ObtenerSesion(idSesion));
            if (sesion.EsError) return sesion.ErrorBaseDatos(TAG);

            var respuesta = Servicio.ActualizarSesion(sesion.Contenido, token, expirationToken);
            if (respuesta.EsExito)
            {
                Repositorio.ActualizarSesion(sesion.Contenido);

                var save = Repositorio.Try(r => r.Save());
                return save.EsError ? save.ErrorBaseDatos(TAG) : new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Permite cerrar una sesión de usuario.
        /// </summary>
        public Respuesta CerrarSesion(int idSesion)
        {
            var sesion = Repositorio.Try(r => r.ObtenerSesion(idSesion));
            if (sesion.EsError) return sesion.ErrorBaseDatos(TAG);

            var respuesta = Servicio.CerrarSesion(sesion.Contenido);
            if (respuesta.EsExito)
            {
                Repositorio.ActualizarSesion(sesion.Contenido);
                var save = Repositorio.Try(r => r.Save());
                return save.EsError ? save.ErrorBaseDatos(TAG) : new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        public Respuesta<List<Sesion>> ObtenerSesionesActivas(string subjectId)
        {
            var sesiones = Repositorio.Try(r => r.ObtenerSesionesActivas(subjectId));
            return sesiones.EsError
                ? sesiones.ErrorBaseDatos<List<Sesion>>(TAG)
                : new Respuesta<List<Sesion>>(sesiones.Contenido);
        }
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        public Respuesta<List<Sesion>> ObtenerSesionesActivas(int idUsuario)
        {
            var sesiones = Repositorio.Try(r => r.ObtenerSesionesActivas(idUsuario));
            return sesiones.EsError
                ? sesiones.ErrorBaseDatos<List<Sesion>>(TAG)
                : new Respuesta<List<Sesion>>(sesiones.Contenido);
        }
        /// <summary>
        /// Obtiene todos los usuarios que coincidan con los prametros proporcionados.
        /// </summary>
        public Respuesta<ConsultaPaginada<UsuarioItem>> ConsultarUsuarios(ConsultarUsuariosModelo parametros)
        {
            var usuarios = Repositorio.Try(r => r.ConsultarUsuarios(parametros));
            return usuarios.EsError
                ? usuarios.ErrorBaseDatos<ConsultaPaginada<UsuarioItem>>(TAG)
                : new Respuesta<ConsultaPaginada<UsuarioItem>>(usuarios.Contenido);
        }

        /// <summary>
        /// Permite la actualización del estado de un usuario.
        /// </summary>
        public Respuesta ActualizarEstado(bool esHabilitado, int idUsuario)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.Id == idUsuario));
            if (usuario.EsError) return usuario.ErrorBaseDatos(TAG);

            var respuesta = Servicio.ActualizarEstado(usuario.Contenido, esHabilitado);
            if (respuesta.EsExito)
            {
                if (respuesta.Contenido)
                {
                    Repositorio.Update(usuario.Contenido);
                    var save = Repositorio.Try(r => r.Save());
                    if (save.EsError) return save.ErrorBaseDatos(TAG);
                }
                return new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Permite la actualización de la cantidad de sesiones que un usuario tiene permitidas simultaneamente.
        /// </summary>
        public Respuesta ActualizarSesionesPermitidas(int sesionesPermitidas, int idUsuario)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.Id == idUsuario));
            if (usuario.EsError) return usuario.ErrorBaseDatos(TAG);

            var respuesta = Servicio.ActualizarCantidadDeSesionesPermitidas(usuario.Contenido, sesionesPermitidas);
            if (respuesta.EsExito)
            {
                if (respuesta.Contenido)
                {
                    Repositorio.Update(usuario.Contenido);
                    var save = Repositorio.Try(r => r.Save());
                    if (save.EsError) return save.ErrorBaseDatos(TAG);
                }
                return new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }

        /// <summary>
        /// Retorna el estado de un usuario
        /// </summary>
        public Respuesta<bool> EsUsuarioHabilitado(string subjectId)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.SubjectId == subjectId));

            if (usuario.EsError) return new Respuesta<bool>(false);

            if (usuario.EsExito)
            {
                if (usuario?.Contenido?.EsHabilitado ?? false)
                {
                    return new Respuesta<bool>(true);
                }

                return new Respuesta<bool>(false);
            }
            else
            {
                return new Respuesta<bool>(false);
            }
        }

        /// <summary>
        /// Obtiene el perfil de un usuario registrado.
        /// </summary>
        public Respuesta<Modelos.Usuario> ObtenerPerfil(int idUsuario)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.Id == idUsuario));
            return usuario.EsError
                ? usuario.ErrorBaseDatos<Modelos.Usuario>(TAG)
                : new Respuesta<Modelos.Usuario>(usuario.Contenido.ToModel<Modelos.Usuario>());
        }
        /// <summary>
        /// Obtiene el perfil de un usuario registrado.
        /// </summary>
        public Respuesta<Modelos.Usuario> ObtenerPerfil(string subjectId)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.SubjectId == subjectId));
            return usuario.EsError
                ? usuario.ErrorBaseDatos<Modelos.Usuario>(TAG)
                : new Respuesta<Modelos.Usuario>(usuario.Contenido.ToModel<Modelos.Usuario>());
        }
        /// <summary>
        /// Administra el agregado o eliminado de roles a un usuario.
        /// </summary>
        public Respuesta<List<RolDeUsuarioBase>> AdministrarRoles(List<RolDeUsuarioBase> roles, string subjectId)
        {
            if (roles == null || roles.Count <= 0) return new Respuesta<List<RolDeUsuarioBase>>("Lista invalida");

            var permisosRoles = ServicioRoles.RecursosDeRolPorUsuarioParticulares(roles, subjectId);
            var respuesta = Servicio.AdministrarRoles(roles, permisosRoles, subjectId);
            if (respuesta.EsExito)
            {
                var paraAgregar = respuesta.Contenido.Where(r => r.Id <= 0).ToList();
                var paraEliminar = respuesta.Contenido.Where(r => r.Id > 0);
                Repositorio.AdministrarRoles(paraAgregar, paraEliminar);

                var usuarioConRolesDirectos = Repositorio.ContarRolesDirectos(roles.FirstOrDefault().IdUsuario);

                var respuestaAdministrar = Repositorio.CompilarRolesPorUsuario(paraAgregar.Select(rn => rn.IdRol), paraEliminar.Select(rr => rr.IdRol), roles.FirstOrDefault().IdUsuario);

                if (respuestaAdministrar.EsExito)
                {
                    using (var tran = new TransactionScope())
                    {
                        var save = Repositorio.Try(r => r.Save());
                        if (save.EsError)
                            return save.ErrorBaseDatos<List<RolDeUsuarioBase>>(TAG);

                        tran.Complete();
                    }
                    return new Respuesta<List<RolDeUsuarioBase>>(paraAgregar.Select(e => new RolDeUsuarioBase
                    {
                        Id = e.Id,
                        IdRol = e.IdRol,
                        IdUsuario = e.IdUsuario,
                        EsAsignado = true
                    }).ToList());
                }
                else
                {
                    return new Respuesta<List<RolDeUsuarioBase>>("");//espuestaAdministrar;
                }
            }
            else
            {
                return new Respuesta<List<RolDeUsuarioBase>>(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los grupos que seran eliminados.
        /// </summary>
        public Respuesta CompilarRolesPorGrupos(IEnumerable<int> removerGrupos)
        {
            return Repositorio.CompilarRolesPorGrupos(removerGrupos);
        }

        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que seran eliminados.
        /// </summary>
        public Respuesta CompilarRolesPorRoles(IEnumerable<int> removerRoles)
        {
            return Repositorio.CompilarRolesPorRoles(removerRoles);
        }

        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los usuarios asignados o removidos del grupo indicado.
        /// </summary>
        public Respuesta CompilarRolesPorAsignacionUsuarios(IEnumerable<int> nuevosUsuarios, IEnumerable<int> removerUsuarios, int idGrupo)
        {
            return Repositorio.CompilarRolesPorAsignacionUsuarios(nuevosUsuarios, removerUsuarios, idGrupo);
        }

        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles asignados o removidos del grupo indicado.
        /// </summary>
        public Respuesta CompilarRolesPorAsignacionRoles(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idGrupo)
        {
            return Repositorio.CompilarRolesPorAsignacionRoles(nuevosRoles, removerRoles, idGrupo);
        }
        /// <summary>
        /// Ejecuta el guardado de los cambios en el repositorio de usuarios, permitiendo capturar las excepciones en caso de su existencia.
        /// </summary>
        public Respuesta GuardarCompilacionRoles()
        {
            Repositorio.Save();
            return new Respuesta();
        }

        /// <summary>
        /// Obtiene los roles que el usuario puede administrar filtrados por los parametros indicados.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase>> ConsultarRoles(ConsultaRol parametros, string subjectId)
        {
            var recursos = Repositorio.Try(r => r.ConsultarRoles(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase>>(TAG);

            return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase>>(recursos.Contenido);


        }

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos
        /// </summary>
        public Respuesta<Dictionary<string, string>> ObtenerNombreUsuarios(List<string> subjectIds)
        {
            var obtenerNombreUsuarios = Repositorio.Try(r => r.ObtenerNombreUsuarios(subjectIds));

            return obtenerNombreUsuarios.EsError
                ? obtenerNombreUsuarios.ErrorBaseDatos<Dictionary<string, string>>(TAG)
                : new Respuesta<Dictionary<string, string>>(obtenerNombreUsuarios.Contenido);
        }

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos por ID
        /// </summary>
        public Respuesta<Dictionary<int, string>> ObtenerNombreUsuarios(List<int> ids)
        {
            var obtenerNombreUsuarios = Repositorio.Try(r => r.ObtenerNombreUsuarios(ids));

            return obtenerNombreUsuarios.EsError
                ? obtenerNombreUsuarios.ErrorBaseDatos<Dictionary<int, string>>(TAG)
                : new Respuesta<Dictionary<int, string>>(obtenerNombreUsuarios.Contenido);
        }

        /// <summary>
        /// Obtiene un usuario según el subjectId dado.
        /// </summary>
        public Respuesta<Dominio.Seguridad.Entidades.Usuario> ObtenerUsuario(string subjectId)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.SubjectId == subjectId));

            if (usuario.EsError)
                return usuario.ErrorBaseDatos<Dominio.Seguridad.Entidades.Usuario>(TAG);

            return Servicio.ObtenerUsuario(usuario.Contenido);
        }

        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        public Respuesta<List<UsuarioItem>> ObtenerUsuarios()
        {
            var usuarios = Repositorio.Try(r => r.ObtenerUsuarios());
            return usuarios.EsError
                ? usuarios.ErrorBaseDatos<List<UsuarioItem>>(TAG)
                : new Respuesta<List<UsuarioItem>>(usuarios.Contenido);
        }

        /// <summary>
        /// Obtiene un usuario según el subjectId dado.
        /// </summary>
        public Respuesta<Dominio.Seguridad.Entidades.Usuario> ObtenerUsuario(int idUsuario)
        {
            var usuario = Repositorio.Try(r => r.Get(u => u.Id == idUsuario, false));

            if (usuario.EsError)
                return usuario.ErrorBaseDatos<Dominio.Seguridad.Entidades.Usuario>(TAG);

            return Servicio.ObtenerUsuario(usuario.Contenido);
        }
        /// <summary>
        /// Metodo que obtiene los usuarios que cumplen con algun rol de una lista de roles
        /// </summary>
        /// <param name="idsRoles"></param>
        /// <returns></returns>
        public Respuesta<List<UsuarioNotificacion>> ObtenerUsuariosPorRoles(List<int> idsRoles)
        {
            var usuarios = Repositorio.Try(r => r.ObtenerUsuariosporRoles(idsRoles));

            if (usuarios.EsError)
            {
                return usuarios.ErrorBaseDatos<List<UsuarioNotificacion>>(usuarios.TAG);
            }

            return new Respuesta<List<UsuarioNotificacion>>(usuarios.Contenido);
        }


        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        public Respuesta<List<UsuarioItem>> ObtenerUsuariosAutocomplete(ConsultarUsuariosModelo consulta)
        {
            var usuarios = Repositorio.Try(r => r.ObtenerUsuariosAutocomplete(consulta));

            return usuarios.EsError
                ? usuarios.ErrorBaseDatos<List<UsuarioItem>>(TAG)
                : new Respuesta<List<UsuarioItem>>(usuarios.Contenido);
        }
    }
}
