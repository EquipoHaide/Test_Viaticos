using Aplicacion.Seguridad.Servicios;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Entidades = Dominio.Seguridad.Entidades;
using Modelos = Dominio.Seguridad.Modelos;

namespace Aplicacion.Seguridad.Servicios
{
    public class ServicioRoles : Nucleo.ServicioRecursoBase<RecursoRol>, Core.IServicioRoles
    {
        const string TAG = "Aplicacion.Seguridad.Servicios.Roles.ServicioRoles";

        Nucleo.IAplicacion App { get; set; }

        Dominio.Seguridad.Servicios.Roles.IServicioRoles servicio;
        Dominio.Seguridad.Servicios.Roles.IServicioRoles Servicio => App.Inject(ref servicio);
        public override IServicioRecursoBase<RecursoRol> ServicioDominio => Servicio;

        IRepositorioRoles repositorio;
        IRepositorioRoles Repositorio => App.Inject(ref repositorio);
        public override IRepositorioRecurso<RecursoRol> RepositorioRecurso => Repositorio;

        IServicioUsuarios servicioUsuarios;
        IServicioUsuarios ServicioUsuarios => App.Inject(ref servicioUsuarios);

        Core.IServicioRoles servicioRoles;
        public override Nucleo.IServicioRoles ServicioDeRoles => App.Inject(ref servicioRoles);

        public ServicioRoles(Nucleo.IAplicacion app)
        {
            App = app;
        }

        #region Aplicacion.Nucleo.IServicioRoles
        /// <summary>
        /// Obtiene los permisos sobre el rol indicado que el usuario indicado posee
        /// </summary>
        public IEnumerable<IPermiso> RecursosDeRolPorUsuario(int idRol, string subjectId)
        {
            if (idRol <= 0) return new List<IPermiso>();
            if (subjectId.IsNullOrEmptyOrWhiteSpace()) return new List<IPermiso>();

            var permisos = Repositorio.Try(r => r.RecursosDeRolPorUsuario(idRol, subjectId));
            if (permisos.EsExito) return permisos.Contenido;

            return new List<IPermiso>();
        }
        /// <summary>
        /// Obtiene los permisos sobre los roles indicados que el usuario indicado posee
        /// </summary>
        public IEnumerable<IPermiso> RecursosDeRolPorUsuario(IEnumerable<IRolItem> roles, string subjectId)
        {
            if (roles == null || roles.Count() <= 0) return new List<IPermiso>();
            if (subjectId.IsNullOrEmptyOrWhiteSpace()) return new List<IPermiso>();

            var permisos = Repositorio.Try(r => r.RecursosDeRolPorUsuario(roles, subjectId));
            if (permisos.EsExito) return permisos.Contenido;

            return new List<IPermiso>();
        }
        /// <summary>
        /// Obtiene los permisos sobre los roles indicados que el usuario indicado posee con sus roles particulares.
        /// </summary>
        public IEnumerable<IPermiso> RecursosDeRolPorUsuarioParticulares(IEnumerable<IRolItem> roles, string subjectId)
        {
            if (roles == null || roles.Count() <= 0) return new List<IPermiso>();
            if (subjectId.IsNullOrEmptyOrWhiteSpace()) return new List<IPermiso>();

            var permisos = Repositorio.Try(r => r.RecursosDeRolPorUsuarioParticulares(roles, subjectId));
            if (permisos.EsExito) return permisos.Contenido;

            return new List<IPermiso>();
        }
        /// <summary>
        /// Obtiene la lista de roles particulares del usuario indicado.
        /// </summary>
        public Respuesta<IEnumerable<RolParticularVista>> ObtenerRolesParticulares(string subjectId)
        {
            var roles = Repositorio.Try(r => r.ObtenerRolesParticulares(subjectId));
            if (roles.EsError) return roles.ErrorBaseDatos<IEnumerable<RolParticularVista>>(TAG);

            return roles;
        }
        /// <summary>
        /// Obtiene la lista de roles directos del usuario indicado.
        /// </summary>
        public Respuesta<IEnumerable<RolDirectoVista>> ObtenerRolesDirectos(string subjectId)
        {
            var roles = Repositorio.Try(r => r.ObtenerRolesDirectos(subjectId));
            if (roles.EsError) return roles.ErrorBaseDatos<IEnumerable<RolDirectoVista>>(TAG);

            return roles;
        }
        /// <summary>
        /// Obtiene la lista de roles del usuario indicado.
        /// </summary>
        public Respuesta<IEnumerable<RolUsuarioVista>> ObtenerRolesUsuarios(string subjectId)
        {
            var roles = Repositorio.Try(r => r.ObtenerRolesUsuarios(subjectId));
            if (roles.EsError) return roles.ErrorBaseDatos<IEnumerable<RolUsuarioVista>>(TAG);

            return roles;
        }
        #endregion
        #region ServicioRecursoBase
        /// <summary>
        /// Obtiene una pagina de permisos del grupo que el usuario puede administrar filtrados por los parametros indicados.
        /// </summary>
        public override Respuesta<ConsultaPaginada<IPermisoModel>> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (ConsultaRecursoRolModelo)parametros;
            if (opciones == null || opciones.IdRol <= 0) return new Respuesta<ConsultaPaginada<IPermisoModel>>(R.strings.ModeloDeConsultaDeRecursosInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarRecursos(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<IPermisoModel>>(TAG);

            return recursos;
        }
        #endregion
        /// <summary>
        /// Agrega un nuevo rol.
        /// </summary>
        public Respuesta<Modelos.Rol> Crear(Modelos.Rol rol, string subjectId)
        {
            if (rol == null) return new Respuesta<Modelos.Rol>(R.strings.RolInvalido, TAG);

            var rolesParticulares = ServicioDeRoles.ObtenerRolesParticulares(subjectId);
            if (!rolesParticulares.EsExito) return new Respuesta<Modelos.Rol>(rolesParticulares.ExcepcionInterna, rolesParticulares.Mensaje, rolesParticulares.TAG);

            var mismoNombre = Repositorio.Try(r => r.ObtenerConMismoNombre(rol.Nombre));
            if (!mismoNombre.EsExito) return mismoNombre.ErrorBaseDatos<Modelos.Rol>(TAG);

            var respuesta = Servicio.Crear(rol.ToEntity<Entidades.Rol>(), mismoNombre.Contenido, rolesParticulares.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                Repositorio.Add(respuesta.Contenido);

                var save = Repositorio.Try(r => r.Save());
                return save.EsError ? save.ErrorBaseDatos<Modelos.Rol>(TAG) : new Respuesta<Modelos.Rol>(respuesta.Contenido.ToModel<Modelos.Rol>());
            }

            return new Respuesta<Modelos.Rol>(respuesta.Mensaje, TAG);
        }
        /// <summary>
        /// Permite la modificacion de un rol
        /// </summary>
        public Respuesta Modificar(Modelos.Rol rol, string subjectId)
        {
            if (rol == null) return new Respuesta(R.strings.RolInvalido, TAG);

            var mismoNombre = Repositorio.Try(r => r.ObtenerConMismoNombre(rol.Nombre, rol.Id));
            if (mismoNombre.EsError) return mismoNombre.ErrorBaseDatos(TAG);

            var permisos = Repositorio.Try(r => r.RecursosDeRolPorUsuario(rol.Id, subjectId));
            if (permisos.EsError) return permisos.ErrorBaseDatos(TAG);

            var rolOriginal = Repositorio.Try(r => r.Get(m => m.Id == rol.Id));
            if (rolOriginal.EsError) return permisos.ErrorBaseDatos(TAG);

            var respuesta = Servicio.Modificar(rol.ToEntity<Entidades.Rol>(rolOriginal.Contenido), mismoNombre.Contenido, permisos.Contenido, subjectId);
            if (respuesta.EsExito)
            {
                Repositorio.Update(respuesta.Contenido);

                var save = Repositorio.Try(r => r.Save());
                return save.EsError ? save.ErrorBaseDatos(TAG) : new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Permite eliminar una lista de roles.
        /// </summary>
        public Respuesta Eliminar(List<Modelos.Rol> roles, string subjectId)
        {
            var rolesOriginales = Repositorio.Try(r => r.ObtenerConRecursos(roles, subjectId));

            if (rolesOriginales.EsError)
                return rolesOriginales.ErrorBaseDatos(TAG);

            var respuesta = Servicio.Eliminar(rolesOriginales.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                try
                {
                    var respuestaCompila = ServicioUsuarios.CompilarRolesPorRoles(rolesOriginales.Contenido.Select(r => r.Id));

                    if (respuestaCompila.EsExito)
                    {
                        using var tran = new TransactionScope();

                        Repositorio.RemoverRolGrupos(roles.Select(r => r.Id).ToList());
                        Repositorio.RemoverRolUsuarios(roles.Select(r => r.Id).ToList());

                        var save = Repositorio.Try(r => r.Save());

                        if (save.EsError)
                            return save.ErrorBaseDatos(TAG);

                        if (ServicioUsuarios.GuardarCompilacionRoles().EsExito)
                        {

                            tran.Complete();
                        }
                        else
                        {
                            return new Respuesta(R.strings.ErrorCompilacionRoles, TAG);
                        }
                    }
                    else
                        return respuestaCompila;
                }
                catch (Exception ex)
                {
                    return new Respuesta(ex, R.strings.ErrorConexionBaseDeDatos, TAG);
                }

                return new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<Modelos.Rol> Obtener(int id, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Modelos.Rol>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRol(id, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<Modelos.Rol>(TAG);
            }

            return new Respuesta<Modelos.Rol>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura el usuario.
        /// </summary>
        public Respuesta<Modelos.Rol> ObtenerPorLectura(int id, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Modelos.Rol>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRolPorLectura(id, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<Modelos.Rol>(TAG);
            }

            return new Respuesta<Modelos.Rol>(consulta.Contenido);
        }

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<ConsultaPaginada<Modelos.Rol>> Consultar(ConsultaRol filtro, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<ConsultaPaginada<Modelos.Rol>>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRoles(filtro, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<ConsultaPaginada<Modelos.Rol>>(TAG);
            }

            return new Respuesta<ConsultaPaginada<Modelos.Rol>>(consulta.Contenido);
        }

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura y ejecucion el usuario a travez de sus roles particulares.
        /// </summary>
        public Respuesta<ConsultaPaginada<Modelos.Rol>> ConsultarPorRolesParticulares(ConsultaRol filtro, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<ConsultaPaginada<Modelos.Rol>>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRolesPorRolesParticulares(filtro, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<ConsultaPaginada<Modelos.Rol>>(TAG);
            }

            return new Respuesta<ConsultaPaginada<Modelos.Rol>>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene una lista de ID y NombreRol a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<List<(int idRol, string Rol)>> ObtenerRoles(List<int> idRoles, string subjectId, bool requerieFiltroPorPermisos = false)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<List<(int idRol, string Rol)>>(R.strings.UsuarioNoProporcionado, TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRoles(idRoles, subjectId, requerieFiltroPorPermisos));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<List<(int idRol, string Rol)>>(TAG);
            }

            return new Respuesta<List<(int idRol, string Rol)>>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene una lista de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<List<Modelos.Rol>> ObtenerRoles(string nombre, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<List<Modelos.Rol>>(R.strings.UsuarioNoProporcionado, TAG);

            var consulta = Repositorio.Try(r => r.ObtenerRoles(nombre, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<List<Modelos.Rol>>(TAG);
            }

            return new Respuesta<List<Modelos.Rol>>(consulta.Contenido);
        }

        /// <summary>
        /// Método que valida permisos de lectura sobre el recurso rol
        /// </summary>
        public Respuesta ValidarPermisosRecursoRol(List<int> ids, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta(R.strings.UsuarioNoProporcionado, TAG);

            var obtenerPermisos = Repositorio.Try(r => r.ObtenerPermisosDeUsuariorRol(ids, subjectId));

            if (obtenerPermisos.EsError)
            {
                App.GetLogger().Log.Error(obtenerPermisos.ExcepcionInterna, obtenerPermisos.Mensaje);
                return obtenerPermisos.ErrorBaseDatos(TAG);
            }

            var respuesta = Servicio.ValidarPermisos(obtenerPermisos.Contenido);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);

            return new Respuesta();
        }

        /// <summary>
        /// Método que valida si los roles solicitados se encuentran activos
        /// </summary>
        public Respuesta<List<Entidades.Rol>> ObtenerRolesPorResponsable(List<int> ids)
        {
            var obtenerRoles = Repositorio.Try(r => r.ObtenerRoles(ids));

            if (obtenerRoles.EsError)
            {
                App.GetLogger().Log.Error(obtenerRoles.ExcepcionInterna, obtenerRoles.Mensaje);
                return obtenerRoles.ErrorBaseDatos<List<Entidades.Rol>>(TAG);
            }

            var respuesta = Servicio.ObtenerRolesPorResponsable(obtenerRoles.Contenido);

            if (respuesta.EsError)
                return new Respuesta<List<Entidades.Rol>>(respuesta.Mensaje, respuesta.TAG);

            return new Respuesta<List<Entidades.Rol>>(obtenerRoles.Contenido);
        }

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura el usuario.
        /// </summary>
        public Respuesta<ConsultaPaginada<Modelos.Rol>> ConsultarRoles(ConsultaRol filtro, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<ConsultaPaginada<Modelos.Rol>>(TAG);

            var consulta = Repositorio.Try(r => r.ConsultarRoles(filtro, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<ConsultaPaginada<Modelos.Rol>>(TAG);
            }

            return new Respuesta<ConsultaPaginada<Modelos.Rol>>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<Entidades.Rol> ObtenerRol(int id, string subjectId)
        {
            var permisos = Repositorio.Try(r => r.RecursosDeRolPorUsuario(id, subjectId));
            if (permisos.EsError) return permisos.ErrorBaseDatos<Entidades.Rol>(TAG);

            var rolOriginal = Repositorio.Try(r => r.Get(m => m.Id == id));
            if (rolOriginal.EsError) return rolOriginal.ErrorBaseDatos<Entidades.Rol>(TAG);

            var respuesta = Servicio.ObtenerRol(rolOriginal.Contenido, permisos.Contenido);

            return respuesta;
        }

        /// <summary>
        /// Obtiene un rol por su id
        /// </summary>
        public Respuesta<Entidades.Rol> ObtenerRol(int id, bool requiereValidacion = true)
        {
            var rolOriginal = Repositorio.Try(r => r.Get(m => m.Id == id));
            if (rolOriginal.EsError) return rolOriginal.ErrorBaseDatos<Entidades.Rol>(TAG);

            if (requiereValidacion)
            {
                var respuesta = Servicio.ObtenerRol(rolOriginal.Contenido);

                return respuesta;
            }

            return rolOriginal;
        }
    }
}
