using Aplicacion.Nucleo;
using Aplicacion.Seguridad.Servicios;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Entidades = Dominio.Seguridad.Entidades;
using Modelos = Dominio.Seguridad.Modelos;
using ServiciosDominio = Dominio.Seguridad.Servicios.Grupos;


namespace Aplicacion.Seguridad.Servicios
{
    class ServicioGrupos : Nucleo.ServicioRecursoBase<RecursoGrupo>, IServicioGrupos
    {
        const string TAG = "Aplicacion.Seguridad.Servicios.Grupos.ServicioGrupos";

        Nucleo.IAplicacion App { get; set; }

        ServiciosDominio.IServicioGrupos servicio;
        ServiciosDominio.IServicioGrupos Servicio => App.Inject(ref servicio);
        public override IServicioRecursoBase<RecursoGrupo> ServicioDominio => this.Servicio;

        IRepositorioGrupos repositorio;
        IRepositorioGrupos Repositorio => App.Inject(ref repositorio);
        public override IRepositorioRecurso<RecursoGrupo> RepositorioRecurso => this.Repositorio;

        IServicioUsuarios servicioUsuarios;
        IServicioUsuarios ServicioUsuarios => App.Inject(ref servicioUsuarios);

        Core.IServicioRoles servicioRoles;
        Core.IServicioRoles ServicioRoles => App.Inject(ref servicioRoles);
        public override IServicioRoles ServicioDeRoles => ServicioRoles;

        public ServicioGrupos(Nucleo.IAplicacion app)
        {
            App = app;
        }

        /// <summary>
        /// Agrega un nuevo grupo.
        /// </summary>
        public Respuesta<Modelos.Grupo> Crear(Modelos.Grupo grupo, string subjectId)
        {
            if (grupo == null)
                return new Respuesta<Modelos.Grupo>(R.strings.GrupoInvalido, TAG);

            var rolesParticulares = ServicioDeRoles.ObtenerRolesParticulares(subjectId);

            if (rolesParticulares.EsError)
                return rolesParticulares.Error<Modelos.Grupo>();

            var mismoNombre = Repositorio.Try(r => r.ObtenerGruposConElMismoNombre(grupo.Nombre));

            if (mismoNombre.EsError)
                return mismoNombre.ErrorBaseDatos<Modelos.Grupo>(TAG);

            var respuesta = Servicio.Crear(grupo.ToEntity<Grupo>(), mismoNombre.Contenido, rolesParticulares.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                Repositorio.Add(respuesta.Contenido);

                var save = Repositorio.Try(r => r.Save());

                if (save.EsError)
                {
                    return save.ErrorBaseDatos<Modelos.Grupo>(TAG);
                }

                return new Respuesta<Modelos.Grupo>(respuesta.Contenido.ToModel<Modelos.Grupo>());
            }

            return new Respuesta<Modelos.Grupo>(respuesta.Mensaje, respuesta.TAG);
        }
        /// <summary>
        /// Permite la modificacion de un grupo
        /// </summary>
        public Respuesta Modificar(Modelos.Grupo grupo, string subjectId)
        {
            if (grupo == null) return new Respuesta(R.strings.GrupoInvalido, TAG);

            var mismoNombre = Repositorio.Try(r => r.ObtenerGruposConElMismoNombre(grupo.Nombre, grupo.Id));
            if (mismoNombre.EsError) return mismoNombre.ErrorBaseDatos(TAG);

            var permisos = Repositorio.Try(r => r.ObtenerPermisosDeUsuario(grupo.Id, subjectId));
            if (permisos.EsError) return permisos.ErrorBaseDatos(TAG);

            var grupoOriginal = Repositorio.Try(r => r.Get(g => g.Id == grupo.Id));
            if (grupoOriginal.EsError) return permisos.ErrorBaseDatos(TAG);

            var respuesta = Servicio.Modificar(grupo.ToEntity<Entidades.Grupo>(grupoOriginal.Contenido), mismoNombre.Contenido, permisos.Contenido, subjectId);
            if (respuesta.EsExito)
            {
                var save = Repositorio.Try(r => r.Save());
                if (save.EsError) return save.ErrorBaseDatos(TAG);

                return new Respuesta();
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Permite eliminar una lista de grupos.
        /// </summary>
        public Respuesta Eliminar(List<Modelos.Grupo> grupos, string subjectId)
        {
            var gruposOriginales = Repositorio.Try(r => r.ObtenerConRecursos(grupos, subjectId));

            if (gruposOriginales.EsError)
                return gruposOriginales.ErrorBaseDatos();

            var respuesta = Servicio.Eliminar(gruposOriginales.Contenido);

            if (respuesta.EsExito)
            {
                Repositorio.Remove(respuesta.Contenido);
                try
                {
                    var respuestaCompilacion = ServicioUsuarios.CompilarRolesPorGrupos(respuesta.Contenido.Select(g => g.Id));

                    if (respuestaCompilacion.EsExito)
                    {
                        using (var tran = new TransactionScope())
                        {
                            Repositorio.Save();

                            if (ServicioUsuarios.GuardarCompilacionRoles().EsExito)
                                tran.Complete();
                            else
                                return new Respuesta(R.strings.ErrorCompilacionRoles, TAG);
                        }

                        return new Respuesta();
                    }
                    else
                    {
                        return respuestaCompilacion;
                    }
                }
                catch (Exception ex)
                {
                    return new Respuesta(ex, R.strings.ErrorConexionBaseDeDatos, TAG);
                }
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }

        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<Modelos.Grupo> Obtener(int id, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Modelos.Grupo>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerGrupo(id, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<Modelos.Grupo>(TAG);
            }

            return new Respuesta<Modelos.Grupo>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura usuario.
        /// </summary>
        public Respuesta<Modelos.Grupo> ObtenerPorLectura(int id, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Modelos.Grupo>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerGrupoPorLectura(id, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<Modelos.Grupo>(TAG);
            }

            return new Respuesta<Modelos.Grupo>(consulta.Contenido);
        }

        /// <summary>
        /// Obtiene una pagina de permisos del grupo que el usuario puede administrar filtrados por los parametros indicados.
        /// </summary>
        public override Respuesta<ConsultaPaginada<IPermisoModel>> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (Modelos.ConsultaRecursoGrupoModelo)parametros;
            if (opciones == null || opciones.IdRol <= 0) return new Respuesta<ConsultaPaginada<IPermisoModel>>(R.strings.ModeloDeConsultaDeRecursosInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarRecursos(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<IPermisoModel>>(TAG);

            return new Respuesta<ConsultaPaginada<IPermisoModel>>(recursos.Contenido);
        }

        /// <summary>
        /// Administra el agregado o eliminado de usuarios a un grupo.
        /// </summary>
        public Respuesta<List<Modelos.UsuarioDeGrupoBase>> AdministrarUsuarios(List<Modelos.UsuarioDeGrupoBase> usuarios, string subjectId)
        {
            if (usuarios == null || usuarios.Count <= 0) return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>("Lista invalida");

            var permisos = Repositorio.Try(r => r.ObtenerPermisos(usuarios.FirstOrDefault().IdGrupo, subjectId));
            if (permisos.EsError) return permisos.ErrorBaseDatos<List<Modelos.UsuarioDeGrupoBase>>(TAG);

            var respuesta = Servicio.AdministrarUsuarios(usuarios, permisos.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                var usuariosEliminar = respuesta.Contenido.Where(u => u.Id > 0);
                var usuariosAgregar = respuesta.Contenido.Where(u => u.Id == 0).ToList();
                Repositorio.AdministrarUsuarios(usuariosAgregar, usuariosEliminar);

                try
                {
                    var respuestaAdministra = ServicioUsuarios.CompilarRolesPorAsignacionUsuarios(usuariosAgregar.Select(u => u.IdUsuario), usuariosEliminar.Select(u => u.IdUsuario), usuarios.FirstOrDefault().IdGrupo);
                    if (respuestaAdministra.EsExito)
                    {
                        using (var tran = new TransactionScope())
                        {

                            Repositorio.Save();
                            if (ServicioUsuarios.GuardarCompilacionRoles().EsExito)
                            {
                                tran.Complete();
                            }
                            else
                            {
                                return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>(R.strings.ErrorCompilacionRoles, TAG);
                            }
                        }

                        return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>(usuariosAgregar.Select(e => new Modelos.UsuarioDeGrupoBase
                        {
                            Id = e.Id,
                            IdGrupo = e.IdGrupo,
                            IdUsuario = e.IdUsuario,
                            EsAsignado = true
                        }).ToList());

                    }

                    else return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>("");//respuestaAdministra;
                }
                catch (Exception ex)
                {
                    return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>(ex, R.strings.ErrorConexionBaseDeDatos, TAG);//new Respuesta(ex, R.strings.ErrorConexionBaseDeDatos, TAG);
                }
            }
            else
            {
                return new Respuesta<List<Modelos.UsuarioDeGrupoBase>>(respuesta.Mensaje, respuesta.TAG);
            }
        }
        /// <summary>
        /// Administra el agregado o eliminado de roles a un grupo
        /// </summary>
        public Respuesta<List<Modelos.RolDeGrupoBase>> AdministrarRoles(List<Modelos.RolDeGrupoBase> roles, string subjectId)
        {
            if (roles == null || roles.Count <= 0) return new Respuesta<List<Modelos.RolDeGrupoBase>>("Lista invalida");

            var permisosSobreGrupo = Repositorio.Try(r => r.ObtenerPermisos(roles.FirstOrDefault().IdGrupo, subjectId));
            if (permisosSobreGrupo.EsError) return permisosSobreGrupo.ErrorBaseDatos<List<Modelos.RolDeGrupoBase>>(TAG);

            var permisosSobreRoles = ServicioDeRoles.RecursosDeRolPorUsuarioParticulares(roles, subjectId);

            var respuesta = Servicio.AdministrarRoles(roles, permisosSobreGrupo.Contenido, permisosSobreRoles, subjectId);

            if (respuesta.EsExito)
            {
                var paraEliminar = respuesta.Contenido.Where(r => r.Id > 0);
                var paraAgregar = respuesta.Contenido.Where(r => r.Id <= 0).ToList();

                Repositorio.AdministrarRoles(paraAgregar, paraEliminar);
                try
                {
                    var respuestaAdministra = ServicioUsuarios.CompilarRolesPorAsignacionRoles(paraAgregar.Select(r => r.IdRol), paraEliminar.Select(r => r.IdRol), roles.FirstOrDefault().IdGrupo);
                    if (respuestaAdministra.EsExito)
                    {
                        using (var tran = new TransactionScope())
                        {
                            Repositorio.Save();
                            if (ServicioUsuarios.GuardarCompilacionRoles().EsExito)
                            {
                                tran.Complete();
                            }
                            else
                            {
                                return new Respuesta<List<Modelos.RolDeGrupoBase>>(R.strings.ErrorCompilacionRoles, TAG);
                            }
                        }

                        return new Respuesta<List<Modelos.RolDeGrupoBase>>(paraAgregar.Select(e => new Modelos.RolDeGrupoBase
                        {
                            Id = e.Id,
                            IdRol = e.IdRol,
                            IdGrupo = e.IdGrupo,
                            EsAsignado = true
                        }).ToList());
                    }
                    else
                        return new Respuesta<List<Modelos.RolDeGrupoBase>>("");//respuestaAdministra;
                }
                catch (Exception ex)
                {
                    return new Respuesta<List<Modelos.RolDeGrupoBase>>(ex, R.strings.ErrorConexionBaseDeDatos, TAG);
                }
            }
            else
            {
                return new Respuesta<List<Modelos.RolDeGrupoBase>>(respuesta.Mensaje, respuesta.TAG);
            }
        }

        /// <summary>
        /// Consulta paginada de Grupos a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Respuesta<ConsultaPaginada<Modelos.Grupo>> Consultar(Modelos.ConsultaGrupo filtro, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<ConsultaPaginada<Modelos.Grupo>>(TAG);

            var consulta = Repositorio.Try(r => r.ObtenerGrupos(filtro, subjectId));

            if (consulta.EsError)
            {
                App.GetLogger().Log.Information(consulta.ExcepcionInterna, "Ocurrio un error inesperado");
                return consulta.ErrorBaseDatos<ConsultaPaginada<Modelos.Grupo>>(TAG);
            }

            return new Respuesta<ConsultaPaginada<Modelos.Grupo>>(consulta.Contenido);
        }

        public Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem>> ConsultarRoles(Modelos.ConsultaRolGrupo parametros, string subjectId)
        {
            var opciones = parametros;
            if (opciones == null || opciones.IdGrupo <= 0) return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem>>(R.strings.ModeloDeConsultaDerolesInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarRoles(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem>>(TAG);

            return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem>>(recursos.Contenido);
        }

        public Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem>> ConsultarUsuarios(Modelos.ConsultaUsuariosGrupo parametros, string subjectId)
        {
            var opciones = parametros;
            if (opciones == null || opciones.IdGrupo <= 0) return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem>>(R.strings.ModeloDeConsultaDerolesInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarUsuarios(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem>>(TAG);

            return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem>>(recursos.Contenido);
        }
    }
}
