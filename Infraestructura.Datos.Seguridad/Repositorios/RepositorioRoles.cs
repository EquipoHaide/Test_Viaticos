using Infraestructura.Datos.Nucleo;
using System;
using System.Collections.Generic;
using Modelos = Dominio.Seguridad.Modelos;
using Entidades = Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Repositorios;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Dominio.Seguridad.Modelos;
using System.Linq;
//using MicroServices.Platform.Extensions;
using Infraestructura.Transversal.Plataforma.Extensiones;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;

namespace Infraestructura.Datos.Seguridad.Repositorios
{
    public class RepositorioRoles : RepositorioRecurso<Entidades.Rol, RecursoRol>, IRepositorioRoles
    {
        public RepositorioRoles(ISeguridadUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }
        /// <summary>
        /// Obtiene los roles particulares del usuario indicado.
        /// </summary>
        public IEnumerable<RolParticularVista> ObtenerRolesParticulares(string subjectId)
        {
            var query = from rp in UnitOfWork.Set<RolParticularVista>()
                        where rp.SubjectId == subjectId
                        select rp;

            return query.ToList();
        }
        /// <summary>
        /// Obtiene los roles directos del usuario indicado.
        /// </summary>
        public IEnumerable<RolDirectoVista> ObtenerRolesDirectos(string subjectId)
        {
            var query = from rp in UnitOfWork.Set<RolDirectoVista>()
                        where rp.SubjectId == subjectId
                        select rp;

            return query.ToList();
        }
        /// <summary>
        /// Obtiene los roles de usuario del usuario indicado.
        /// </summary>
        public IEnumerable<RolUsuarioVista> ObtenerRolesUsuarios(string subjectId)
        {
            var query = from rp in UnitOfWork.Set<RolUsuarioVista>()
                        where rp.SubjectId == subjectId
                        select rp;

            return query.ToList();
        }
        /// <summary>
        /// Obtiene los recursos del rol indicado para el usuario establecido.
        /// </summary>
        public IEnumerable<RecursoRol> RecursosDeRolPorUsuario(int idRol, string subjectId)
        {
            var rolesDeUsuario = from rp in UnitOfWork.Set<RolUsuarioVista>()
                                 where rp.SubjectId == subjectId
                                 select rp;

            var recursosDeUsuario = (from r in UnitOfWork.Set<RecursoRol>()
                                     join ru in rolesDeUsuario
                                         on r.IdRol equals ru.IdRol
                                     where r.IdRecurso == idRol
                                     select r).AsEnumerable();

            //recursosDeUsuario = from ru in recursosDeUsuario
            //                    group ru by ru.IdRecurso into recursos
            //                    select recursos.FirstOrDefault();



            //var query = from ru in UnitOfWork.Set<RolUsuarioVista>()
            //            join r in UnitOfWork.Set<RecursoRol>()
            //                on ru.IdRol equals r.IdRol
            //            where ru.SubjectId == subjectId && r.IdRecurso == idRol
            //            group r by r.IdRecurso into recursos
            //            select recursos.FirstOrDefault();

            return recursosDeUsuario;
        }
        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido.
        /// </summary>
        public IEnumerable<RecursoRol> RecursosDeRolPorUsuario(IEnumerable<IRolItem> roles, string subjectId)
        {
            var rolesDeUsuario = from rp in UnitOfWork.Set<RolUsuarioVista>()
                                 where rp.SubjectId == subjectId
                                 select rp;

            var recursosDeUsuario = (from r in UnitOfWork.Set<RecursoRol>()
                                     join ru in rolesDeUsuario
                                         on r.IdRol equals ru.IdRol
                                     where roles.Select(r => r.IdRol).Contains(r.IdRecurso)
                                     select r).Distinct();

            return recursosDeUsuario.ToList();
        }
        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido en sus roles particulares.
        /// </summary>
        public IEnumerable<RecursoRol> RecursosDeRolPorUsuarioParticulares(IEnumerable<IRolItem> roles, string subjectId)
        {
            var rolesDeUsuario = from rp in UnitOfWork.Set<RolParticularVista>()
                                 where rp.SubjectId == subjectId
                                 select rp.IdRol;

            var recursosDeUsuario = (from r in UnitOfWork.Set<RecursoRol>()
                                     join ru in rolesDeUsuario
                                         on r.IdRol equals ru
                                     where roles.Select(r => r.IdRol).Contains(r.IdRecurso)
                                     select r).Distinct();

            return recursosDeUsuario.ToList();
        }
        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public override ConsultaPaginada<IPermisoModel> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (ConsultaRecursoRolModelo)parametros;
            if (opciones == null || opciones.IdRol <= 0) return null;//debe especificar almenos el rol que se administrara.


            var recursosDeUsuario = ObtenerRecursosDelUsuario(subjectId);


            var recursosDelRolAdministrado = from r in UnitOfWork.Set<RecursoRol>()
                                             where r.IdRol == opciones.IdRol
                                             select r;

            IQueryable<RecursoDeRol> query = null;
            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables.
            {
                query = from ru in recursosDeUsuario
                        join rr in recursosDelRolAdministrado
                            on ru.IdRecurso equals rr.IdRecurso into rt
                        from rtt in rt.DefaultIfEmpty()
                        select new RecursoDeRol()
                        {
                            Id = rtt == null ? 0 : rtt.Id,
                            IdRol = opciones.IdRol,
                            IdRecurso = ru.IdRecurso,

                            EsLectura = rtt == null ? false : rtt.EsLectura,
                            EsEscritura = rtt == null ? false : rtt.EsEscritura,
                            EsEjecucion = rtt == null ? false : rtt.EsEjecucion,

                            FechaAsignacion = rtt == null ? (DateTime?)null : rtt.FechaCreacion,

                            Nombre = ru.Recurso.Nombre,
                            Descripcion = ru.Recurso.Descripcion,
                            EsAsignado = rtt != null
                        };
            }
            else if ((bool)opciones.EsAsignado) //Asignados Administrables
            {
                if (!opciones.EsLectura && !opciones.EsEscritura && !opciones.EsEjecucion)
                {
                    query = from ru in recursosDeUsuario
                            join rr in recursosDelRolAdministrado
                                on ru.IdRecurso equals rr.IdRecurso into arr
                            from rrAdmin in arr
                            where rrAdmin.EsLectura || rrAdmin.EsEscritura || rrAdmin.EsEjecucion
                            select new RecursoDeRol()
                            {
                                Id = rrAdmin.Id,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = rrAdmin.EsLectura,
                                EsEscritura = rrAdmin.EsEscritura,
                                EsEjecucion = rrAdmin.EsEjecucion,

                                FechaAsignacion = rrAdmin.FechaCreacion,

                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion,
                                EsAsignado = true
                            };
                }
                else
                {
                    query = from ru in recursosDeUsuario
                            join rr in recursosDelRolAdministrado
                                on ru.IdRecurso equals rr.IdRecurso into arr
                            from rrAdmin in arr
                            where rrAdmin.EsLectura == opciones.EsLectura
                            && rrAdmin.EsEscritura == opciones.EsEscritura
                            && rrAdmin.EsEjecucion == opciones.EsEjecucion
                            select new RecursoDeRol()
                            {
                                Id = rrAdmin.Id,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = rrAdmin.EsLectura,
                                EsEscritura = rrAdmin.EsEscritura,
                                EsEjecucion = rrAdmin.EsEjecucion,

                                FechaAsignacion = rrAdmin.FechaCreacion,

                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion,
                                EsAsignado = true
                            };
                }

            }
            else //Los administrables no asingnados
            {
                if (recursosDelRolAdministrado.Count() <= 0)
                {
                    query = from ru in recursosDeUsuario
                            select new RecursoDeRol()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = false,
                                EsEscritura = false,
                                EsEjecucion = false,

                                FechaAsignacion = (DateTime?)null,

                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion,
                                EsAsignado = false
                            };
                }
                else
                {
                    query = from ru in recursosDeUsuario
                            where !recursosDelRolAdministrado.Select(rra => rra.IdRecurso).Contains(ru.IdRecurso)
                            select new RecursoDeRol()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = false,
                                EsEscritura = false,
                                EsEjecucion = false,

                                FechaAsignacion = (DateTime?)null,

                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion,
                                EsAsignado = false
                            };
                }
            }

            if (opciones.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                query = BusquedaGenerica(query, opciones.Query);
            }
            else
            {
                query = BusquedaAvanzada(query, opciones);
            }

            var resultado = query.Distinct().AsEnumerable().OrderByDescending(r => r.EsAsignado).ThenBy(r => r.FechaAsignacion);

            return resultado.AsQueryable<IPermisoModel>().Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Modelos.Rol ObtenerRol(int id, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var rolesQuery = from rr in UnitOfWork.Set<RecursoRol>()
                             join ru in rolesUsuario
                                on rr.IdRol equals ru.IdRol
                             join r in Set
                                on rr.IdRecurso equals r.Id
                             where r.Activo && rr.EsLectura && rr.EsEscritura
                             select r;

            rolesQuery = (from r in rolesQuery
                          where r.Id == id
                          select r);

            if (rolesQuery.Count() == 0)
                return null;

            return (from r in rolesQuery
                    select new Modelos.Rol
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura usuario.
        /// </summary>
        public Modelos.Rol ObtenerRolPorLectura(int id, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var rolesQuery = from rr in UnitOfWork.Set<RecursoRol>()
                             join ru in rolesUsuario
                                on rr.IdRol equals ru.IdRol
                             join r in Set
                                on rr.IdRecurso equals r.Id
                             where r.Activo && rr.EsLectura
                             select r;

            rolesQuery = (from r in rolesQuery
                          where r.Id == id
                          select r);

            if (rolesQuery.Count() == 0)
                return null;

            return (from r in rolesQuery
                    select new Modelos.Rol
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Consulta paginada de Roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public ConsultaPaginada<Modelos.Rol> ObtenerRoles(ConsultaRol filtro, string subjectId)
        {
            if (filtro == null) return null;

            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var rolesQuery = (from rr in UnitOfWork.Set<RecursoRol>()
                              join ru in rolesUsuario
                                 on rr.IdRol equals ru.IdRol
                              join r in Set
                                 on rr.IdRecurso equals r.Id
                              where r.Activo && rr.EsLectura
                              select r).Distinct();

            if (filtro.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                rolesQuery = (from r in rolesQuery
                              where r.Nombre.ToLower().Contains(filtro.Query.ToLower())
                               || r.Descripcion.ToLower().Contains(filtro.Query.ToLower())
                              select r);
            }
            else
            {
                if (filtro.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Nombre.ToLower().Contains(filtro.Nombre.ToLower())
                                  select r);
                }

                if (filtro.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Descripcion.ToLower().Contains(filtro.Descripcion.ToLower())
                                  select r);
                }
            }

            var roles = from r in rolesQuery
                        select new Modelos.Rol
                        {
                            Id = r.Id,
                            Nombre = r.Nombre,
                            Descripcion = r.Descripcion
                        };


            return roles.Paginar(filtro.Pagina, filtro.ElementosPorPagina);
        }
        /// <summary>
        /// Obtiene los recursos de grupos a los que tiene alcance el usuario con sus roles particulares.
        /// </summary>
        private IQueryable<RecursoRol> ObtenerRecursosDelUsuario(string subjectId)
        {
            var particulares = from rp in UnitOfWork.Set<RolParticularVista>()
                               where rp.SubjectId == subjectId
                               select rp.IdRol;

            var recursosDeUsuario = from r in UnitOfWork.Set<RecursoRol>()
                                    join rp in particulares
                                        on r.IdRol equals rp
                                    where r.EsLectura && r.Recurso.Activo //&& r.EsEjecucion
                                    select r;

            return recursosDeUsuario;
        }
        /// <summary>
        /// Filtra los recursos donde el nombre o la descripcion cohincida con el parametro Query
        /// </summary>
        private IQueryable<RecursoDeRol> BusquedaGenerica(IQueryable<RecursoDeRol> recursos, string parametro)
        {
            return from r in recursos
                   where r.Nombre.ToLower().Contains(parametro.ToLower())
                   || r.Descripcion.ToLower().Contains(parametro.ToLower())
                   select r;
        }
        /// <summary>
        /// Filtr los recursos por el nombre la descripcion o por la sfechas de inicio o termino especificadas.
        /// </summary>
        private IQueryable<RecursoDeRol> BusquedaAvanzada(IQueryable<RecursoDeRol> recursos, ConsultaRecursoRolModelo opciones)
        {
            if (opciones.Nombre.IsNotNullOrEmptyOrWhiteSpace())
            {
                recursos = from r in recursos
                           where r.Nombre.ToLower().Contains(opciones.Nombre.ToLower())
                           select r;
            }
            if (opciones.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
            {
                recursos = from r in recursos
                           where r.Descripcion.ToLower().Contains(opciones.Descripcion.ToLower())
                           select r;
            }

            if (opciones.Inicio != null || opciones.Fin != null)
            {
                if (opciones.Inicio != null && opciones.Fin == null) //Desde
                {
                    recursos = from r in recursos
                               where r.FechaAsignacion.Value.Date >= opciones.Inicio.Value.Date
                               select r;
                }
                else if (opciones.Inicio == null && opciones.Fin != null) //hasta
                {
                    recursos = from r in recursos
                               where r.FechaAsignacion.Value.Date <= opciones.Fin.Value.Date
                               select r;
                }
                else//entre
                {
                    recursos = from r in recursos
                               where r.FechaAsignacion.Value.Date >= opciones.Inicio.Value.Date && r.FechaAsignacion.Value.Date <= opciones.Fin.Value.Date
                               select r;
                }
            }

            return recursos;
        }
        /// <summary>
        /// Obtiene un contador con la cantidad de roles que tienen el mismo nombre exceptuando el rol indicado 
        /// </summary>
        public int ObtenerConMismoNombre(string nombre, int idRol = 0)
        {
            if (nombre.IsNullOrEmptyOrWhiteSpace()) return 0;

            return (from r in Set
                    where r.Nombre.Trim().ToLower() == nombre.Trim().ToLower()
                    && r.Activo
                    && r.Id != idRol
                    select r.Id).Count();
        }
        /// <summary>
        /// Obtiene una lista de roles cargados con los permisos que el usuario posee sobre ellos.
        /// </summary>
        public IEnumerable<Entidades.Rol> ObtenerConRecursos(IEnumerable<Modelos.Rol> roles, string subjectId)
        {
            if (roles == null) return null;

            var rolesDeUsuario = (from r in UnitOfWork.Set<RolUsuarioVista>()
                                  where r.SubjectId == subjectId
                                  select r.IdRol).ToList();

            var idsRoles = roles.Select(g => g.Id);

            return (from r in Set
                    where idsRoles.Contains(r.Id) && r.Activo
                    select new
                    {
                        rol = r,
                        recursos = r.Recursos.Where(rec => rolesDeUsuario.Contains(rec.IdRol))
                    }).ToList().Select(r => r.rol);
        }
        /// <summary>
        /// Obtiene la lista de roles que el usuario puede leer con sus roles particulares. 
        /// </summary>
        public ConsultaPaginada<Modelos.Rol> ObtenerRolesPorRolesParticulares(ConsultaRol filtro, string subjectId)
        {
            if (filtro == null) return null;

            var rolesUsuario = from ru in UnitOfWork.Set<RolParticularVista>()
                               where ru.SubjectId == subjectId
                               select ru.IdRol;

            var rolesQuery = from ru in rolesUsuario
                             join per in UnitOfWork.Set<RecursoRol>()
                                  on ru equals per.IdRol
                             join rol in Set
                                  on per.IdRecurso equals rol.Id
                             where per.EsLectura && rol.Activo
                             select rol;

            if (filtro.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                rolesQuery = (from r in rolesQuery
                              where r.Nombre.ToLower().Contains(filtro.Query.ToLower())
                               || r.Descripcion.ToLower().Contains(filtro.Query.ToLower())
                              select r);
            }
            else
            {
                if (filtro.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Nombre.ToLower() == filtro.Nombre.ToLower()
                                  select r);
                }

                if (filtro.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Descripcion.ToLower() == filtro.Descripcion.ToLower()
                                  select r);
                }
            }

            var roles = from r in rolesQuery
                        select new Modelos.Rol
                        {
                            Id = r.Id,
                            Nombre = r.Nombre,
                            Descripcion = r.Descripcion
                        };

            return roles.Paginar(filtro.Pagina, filtro.ElementosPorPagina);
        }

        /// <summary>
        /// Remueve roles de grupo a los roles.
        /// </summary>
        public void RemoverRolGrupos(List<int> ids)
        {
            if (ids is null || !ids.Any())
                return;

            var gruposRoles = (from g in Set
                               where ids.Contains(g.Id) && g.Activo
                               select g).SelectMany(g => g.Grupos).ToList();

            if (!(gruposRoles is null) || gruposRoles.Any())
            {
                var grupos = UnitOfWork.Set<Entidades.RolGrupo>();

                grupos.RemoveRange(gruposRoles);
            }

            return;
        }

        /// <summary>
        /// Remueve roles de usuario a los roles.
        /// </summary>
        public void RemoverRolUsuarios(List<int> ids)
        {
            if (ids is null || !ids.Any())
                return;

            var usuariosRoles = (from g in Set
                                 where ids.Contains(g.Id) && g.Activo
                                 select g).SelectMany(g => g.Usuarios).ToList();

            if (!(usuariosRoles is null) || usuariosRoles.Any())
            {
                var usuarios = UnitOfWork.Set<Entidades.RolUsuario>();

                usuarios.RemoveRange(usuariosRoles);
            }

            return;
        }

        /// <summary>
        /// Obtiene una lista de ID y NombreRol a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public List<(int idRol, string Rol)> ObtenerRoles(List<int> idRoles, string subjectId, bool requerieFiltroPorPermisos = false)
        {
            if (idRoles is null || !idRoles.Any())
                return new List<(int idRol, string Rol)>();

            if (requerieFiltroPorPermisos)
            {
                var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                                   where ru.SubjectId == subjectId
                                   select ru;

                var rolesQuery = (from rr in UnitOfWork.Set<RecursoRol>()
                                  join ru in rolesUsuario
                                     on rr.IdRol equals ru.IdRol
                                  join r in Set
                                     on rr.IdRecurso equals r.Id
                                  where r.Activo && rr.EsLectura /*&& rr.EsEscritura*/
                                  select r).Distinct();

                rolesQuery = from r in rolesQuery
                             where idRoles.Contains(r.Id)
                             select r;

                return rolesQuery.AsEnumerable().Select(r => (r.Id, r.Nombre)).ToList();
            }

            var query = (from rr in Set
                         where rr.Activo && idRoles.Contains(rr.Id)
                         select rr);

            var resultado = query.AsEnumerable().Select(r => (r.Id, r.Nombre)).ToList();

            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public List<Modelos.Rol> ObtenerRoles(string nombre, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var rolesQuery = (from rr in UnitOfWork.Set<RecursoRol>()
                              join ru in rolesUsuario
                                 on rr.IdRol equals ru.IdRol
                              join r in Set
                                 on rr.IdRecurso equals r.Id
                              where r.Activo && rr.EsLectura /*&& rr.EsEscritura*/
                              select r).Distinct();

            if (nombre.IsNotNullOrEmptyOrWhiteSpace())
            {
                rolesQuery = (from r in rolesQuery
                              where r.Nombre.Trim().ToLower().Contains(nombre.Trim().ToLower())
                              select r);
            }

            var roles = (from r in rolesQuery
                         select new Modelos.Rol
                         {
                             Id = r.Id,
                             Nombre = r.Nombre,
                             Descripcion = r.Descripcion
                         }).ToList();

            return roles;
        }

        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido.
        /// </summary>
        public IEnumerable<RecursoRol> ObtenerPermisosDeUsuariorRol(List<int> idsRol, string subjectId)
        {
            if (idsRol is null || !idsRol.Any() || subjectId.IsNullOrEmptyOrWhiteSpace())
                return null;

            var rolesDeUsuario = from rp in UnitOfWork.Set<RolUsuarioVista>()
                                 where rp.SubjectId == subjectId
                                 select rp;

            var recursosDeUsuario = (from r in UnitOfWork.Set<RecursoRol>()
                                     join ru in rolesDeUsuario
                                         on r.IdRol equals ru.IdRol
                                     where idsRol.Contains(r.IdRecurso)
                                     select r).AsEnumerable();

            return recursosDeUsuario;
        }

        /// <summary>
        /// Obtiene los roles indicados
        /// </summary>
        public List<Entidades.Rol> ObtenerRoles(List<int> idsRol)
        {
            if (idsRol is null || !idsRol.Any())
                return new List<Entidades.Rol>();

            var roles = (from r in Set
                         where idsRol.Contains(r.Id)
                         select r).ToList();

            return roles;
        }

        /// <summary>
        /// Consulta paginada de Roles a los que tiene permiso de lectura el usuario.
        /// </summary>
        public ConsultaPaginada<Modelos.Rol> ConsultarRoles(ConsultaRol filtro, string subjectId)
        {
            if (filtro == null) return null;

            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var rolesQuery = (from rr in UnitOfWork.Set<RecursoRol>()
                              join ru in rolesUsuario
                                 on rr.IdRol equals ru.IdRol
                              join r in Set
                                 on rr.IdRecurso equals r.Id
                              where r.Activo && rr.EsLectura
                              select r).Distinct();

            if (filtro.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                rolesQuery = (from r in rolesQuery
                              where r.Nombre.ToLower().Contains(filtro.Query.ToLower())
                               || r.Descripcion.ToLower().Contains(filtro.Query.ToLower())
                              select r);
            }
            else
            {
                if (filtro.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Nombre.ToLower().Contains(filtro.Nombre.ToLower())
                                  select r);
                }

                if (filtro.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
                {
                    rolesQuery = (from r in rolesQuery
                                  where r.Descripcion.ToLower().Contains(filtro.Descripcion.ToLower())
                                  select r);
                }
            }

            var roles = from r in rolesQuery
                        select new Modelos.Rol
                        {
                            Id = r.Id,
                            Nombre = r.Nombre,
                            Descripcion = r.Descripcion
                        };


            return roles.Paginar(filtro.Pagina, filtro.ElementosPorPagina);
        }
    }
}
