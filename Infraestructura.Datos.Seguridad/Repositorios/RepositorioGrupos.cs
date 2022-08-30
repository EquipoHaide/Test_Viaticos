using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Datos.Nucleo;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Entidades = Dominio.Seguridad.Entidades;
using Modelos = Dominio.Seguridad.Modelos;


namespace Infraestructura.Datos.Seguridad.Repositorios
{
    public class RepositorioGrupos : RepositorioRecurso<Entidades.Grupo, RecursoGrupo>, IRepositorioGrupos
    {
        public RepositorioGrupos(ISeguridadUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }
        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public override ConsultaPaginada<IPermisoModel> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (ConsultaRecursoGrupoModelo)parametros;
            if (opciones == null || opciones.IdRol <= 0) return null;//debe especificar almenos el rol que se administrara.


            var recursosDeUsuario = ObtenerRecursosDelUsuarioPor(opciones, subjectId);


            var recursosDelRolAdministrado = from r in UnitOfWork.Set<RecursoGrupo>()
                                             where r.IdRol == opciones.IdRol
                                             select r;

            IQueryable<RecursoDeGrupo> query = null;
            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables.
            {
                query = from ru in recursosDeUsuario
                        join rr in recursosDelRolAdministrado
                            on ru.IdRecurso equals rr.IdRecurso into rt
                        from rtt in rt.DefaultIfEmpty()
                        select new RecursoDeGrupo()
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
                            select new RecursoDeGrupo()
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
                            select new RecursoDeGrupo()
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
                            select new RecursoDeGrupo()
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
                            select new RecursoDeGrupo()
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
                query = BusquedaAvanzada(query, opciones, subjectId);
            }

            var resultado = query.Distinct().AsEnumerable().OrderByDescending(r => r.EsAsignado).ThenBy(r => r.FechaAsignacion);

            return resultado.AsQueryable<IPermisoModel>().Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }
        /// <summary>
        /// Obtiene la lista de permisos del usuario en sus roles particulares.
        /// </summary>
        public List<RecursoGrupo> ObtenerPermisos(int idGrupo, string subjectId)
        {
            var rolesParticulares = from rp in UnitOfWork.Set<RolParticularVista>()
                                    where rp.SubjectId == subjectId
                                    select rp;

            var permisos = from rp in rolesParticulares
                           join rg in UnitOfWork.Set<RecursoGrupo>()
                               on rp.IdRol equals rg.IdRol
                           where rg.IdRecurso == idGrupo
                           select rg;
            return permisos.ToList();
        }

        /// <summary>
        /// Agrega y elimina las listas de usuarios del grupo.
        /// </summary>
        public void AdministrarUsuarios(IEnumerable<UsuarioGrupo> paraAgregar, IEnumerable<UsuarioGrupo> paraEliminar)
        {
            var usuariosDelGrupo = UnitOfWork.Set<UsuarioGrupo>();

            if (paraAgregar != null && paraAgregar.Count() > 0)
            {
                usuariosDelGrupo.AddRange(paraAgregar);
            }
            if (paraEliminar != null && paraEliminar.Count() > 0)
            {
                usuariosDelGrupo.RemoveRange(paraEliminar);
            }
        }
        /// <summary>
        /// Obtiene la lista de los roles que el grupo posee
        /// </summary>
        public List<int> ObtenerRolesDelGrupo(int idGrupo)
        {
            return (from rg in UnitOfWork.Set<RolGrupo>()
                    where rg.IdGrupo == idGrupo
                    select rg.IdRol).ToList();
        }
        /// <summary>
        /// Cuenta cuantos roles directos tiene cada uno de los usuarios de la lista.
        /// Nota: El idUsuario que regresa el es subjectId
        /// </summary>
        public IEnumerable<UsuarioRolesDirectosContador> ContarRolesDirectos(IEnumerable<UsuarioRolesDirectosContador> usuarios)
        {
            var usuariosTemp = from u in UnitOfWork.Set<Dominio.Seguridad.Entidades.Usuario>()
                                   //where usuarios.Select(us => us.IdUsuario).Contains(u.Id)
                               join usr in usuarios
                                   on u.Id equals usr.IdUsuario
                               select new { IdUsuario = u.Id, IdSubject = u.SubjectId, EsNuevo = usr.EsNuevo };

            var usuariosDirectos = from ud in UnitOfWork.Set<RolDirectoVista>()
                                   join users in usuariosTemp
                                       on ud.SubjectId equals users.IdSubject
                                   select ud;

            var usuariosConRoles = usuariosDirectos.GroupBy(u => u.SubjectId)
                                                   .Select(g => new UsuarioRolesDirectosContador() { SubjectId = g.Key, Roles = g.Count() })
                                                   .ToList();

            return from ut in usuariosTemp
                   join ur in usuariosConRoles
                       on ut.IdSubject equals ur.SubjectId into uCount
                   from r in uCount.DefaultIfEmpty()
                   select new UsuarioRolesDirectosContador()
                   {
                       IdUsuario = ut.IdUsuario,
                       SubjectId = ut.IdSubject,
                       Roles = r == null ? 0 : r.Roles,
                       EsNuevo = ut.EsNuevo
                   };
        }
        /// <summary>
        /// Cuenta cuantos roles directos tiene cada uno de los usuarios del grupo.
        /// Nota: El idUsuario que regresa el es subjectId
        /// </summary>
        public IEnumerable<UsuarioRolesDirectosContador> ContarRolesDirectos(int idGrupo)
        {
            var usuariosTemp = from u in UnitOfWork.Set<UsuarioGrupo>()
                               where u.IdGrupo == idGrupo
                               select u.Usuario.SubjectId;

            var usuariosDirectos = from ud in UnitOfWork.Set<RolDirectoVista>()
                                   join users in usuariosTemp
                                       on ud.SubjectId equals users
                                   select ud;
            return usuariosDirectos.GroupBy(u => u.SubjectId)
                            .Select(g => new UsuarioRolesDirectosContador() { SubjectId = g.Key, Roles = g.Count() })
                            .ToList();
        }
        /// <summary>
        /// Agrega roles a la vista de roles de usuario.
        /// </summary>
        public void AgregarRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas)
        {
            if (rolUsuarioVistas == null || rolUsuarioVistas.Count() <= 0) return;

            UnitOfWork.Set<RolUsuarioVista>().AddRange(rolUsuarioVistas);
        }
        /// <summary>
        /// Agrega roles a la vista de roles particulares.
        /// </summary>
        public void AgregarRolesParticulares(IEnumerable<RolParticularVista> rolUsuarioVistas)
        {
            if (rolUsuarioVistas == null || rolUsuarioVistas.Count() <= 0) return;

            UnitOfWork.Set<RolParticularVista>().AddRange(rolUsuarioVistas);
        }
        /// <summary>
        /// Remueve roles a la vista de roles de usuario.
        /// </summary>
        public void RemoverRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas)
        {
            if (rolUsuarioVistas == null || rolUsuarioVistas.Count() <= 0) return;

            UnitOfWork.Set<RolUsuarioVista>().RemoveRange(rolUsuarioVistas);
        }
        /// <summary>
        /// Remueve roles a la vista de roles particulares.
        /// </summary>
        public void RemoverRolesParticulares(IEnumerable<RolParticularVista> rolUsuarioVistas)
        {
            if (rolUsuarioVistas == null || rolUsuarioVistas.Count() <= 0) return;

            UnitOfWork.Set<RolParticularVista>().RemoveRange(rolUsuarioVistas);
        }
        /// <summary>
        /// Agrega y elimina los roles del grupo.
        /// </summary>
        public void AdministrarRoles(IEnumerable<RolGrupo> paraAgregar, IEnumerable<RolGrupo> paraEliminar)
        {
            var roles = UnitOfWork.Set<RolGrupo>();

            if (paraAgregar != null || paraAgregar.Count() > 0)
            {
                roles.AddRange(paraAgregar);
            }
            if (paraEliminar != null || paraEliminar.Count() > 0)
            {
                roles.RemoveRange(paraEliminar);
            }
        }
        /// <summary>
        /// Obtiene la cantidad de grupos que tengan el mismo nombre. En el caso de recibir el Id del grupo omite ese grupo
        /// </summary>
        public int ObtenerGruposConElMismoNombre(string nombre, int idGrupo = 0)
        {
            var query = from g in Set
                        where g.Nombre.Trim().ToLower() == (nombre == null ? "" : nombre.Trim().ToLower())
                        && g.Activo
                        && g.Id != idGrupo
                        select g;
            return query.Count();
        }
        /// <summary>
        /// Obtiene los permisos que el usuario posee en sus roles de usuario sobre el grupo.
        /// </summary>
        public IEnumerable<RecursoGrupo> ObtenerPermisosDeUsuario(int idGrupo, string subjectId)
        {
            var rolesDeUsuario = from r in UnitOfWork.Set<RolUsuarioVista>()
                                 where r.SubjectId == subjectId
                                 select r;

            return (from p in UnitOfWork.Set<RecursoGrupo>()
                    join r in rolesDeUsuario
                        on p.IdRol equals r.IdRol
                    where p.IdRecurso == idGrupo
                    select p).ToList();
        }
        /// <summary>
        /// Obtiene los grupos indicados cargados con los recursos que el usuario posee con sus roles de usuario.
        /// </summary>  
        public IEnumerable<Entidades.Grupo> ObtenerConRecursos(IEnumerable<Dominio.Seguridad.Modelos.Grupo> grupos, string subjectId)
        {
            if (grupos == null) return null;

            var rolesDeUsuario = (from r in UnitOfWork.Set<RolUsuarioVista>()
                                  where r.SubjectId == subjectId
                                  select r.IdRol).ToList();

            var idsGrupos = grupos.Select(g => g.Id);

            return (from g in Set
                    where idsGrupos.Contains(g.Id)
                    select new
                    {
                        grupo = g,
                        recursos = g.Recursos.Where(rec => rolesDeUsuario.Contains(rec.IdRol))
                    }).ToList().Select(g => g.grupo);
        }

        /// <summary>
        /// Consulta paginada de Grupos a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public ConsultaPaginada<Modelos.Grupo> ObtenerGrupos(ConsultaGrupo filtro, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var gruposQuery = (from r in UnitOfWork.Set<RecursoGrupo>()
                               join ru in rolesUsuario
                                  on r.IdRol equals ru.IdRol
                               join g in Set
                                  on r.IdRecurso equals g.Id
                               where g.Activo && r.EsLectura //&& r.EsEscritura
                               select g).Distinct();

            if (filtro.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                gruposQuery = (from g in gruposQuery
                               where g.Nombre.ToLower().Contains(filtro.Query.ToLower())
                               || g.Descripcion.ToLower().Contains(filtro.Query.ToLower())
                               select g);
            }
            else
            {
                if (filtro.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    gruposQuery = (from g in gruposQuery
                                   where g.Nombre.ToLower().Contains(filtro.Nombre.ToLower())
                                   select g);
                }

                if (filtro.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
                {
                    gruposQuery = (from g in gruposQuery
                                   where g.Descripcion.ToLower().Contains(filtro.Descripcion.ToLower())
                                   select g);
                }
            }

            var grupos = (from g in gruposQuery.AsEnumerable()
                          select new Modelos.Grupo
                          {
                              Id = g.Id,
                              Nombre = g.Nombre,
                              Descripcion = g.Descripcion
                          });

            return grupos.AsEnumerable().Paginar(filtro.Pagina, filtro.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene los recursos de grupos a los que tiene alcance el usuario con sus roles particulares.
        /// </summary>
        private IQueryable<RecursoGrupo> ObtenerRecursosDelUsuarioPor(ConsultaRecursoGrupoModelo opciones, string subjectId)
        {
            var particulares = from rp in UnitOfWork.Set<RolParticularVista>()
                               where rp.SubjectId == subjectId
                               select rp.IdRol;

            var recursosDeUsuario = from r in UnitOfWork.Set<RecursoGrupo>()
                                    join rp in particulares
                                        on r.IdRol equals rp
                                    where r.EsLectura && r.EsEjecucion && r.Recurso.Activo
                                    select r;

            return recursosDeUsuario;
        }
        /// <summary>
        /// Filtra los recursos donde el nombre o la descripcion cohincida con el parametro Query
        /// </summary>
        private IQueryable<RecursoDeGrupo> BusquedaGenerica(IQueryable<RecursoDeGrupo> recursos, string parametro)
        {
            return from r in recursos
                   where r.Nombre.ToLower().Contains(parametro.ToLower())
                   || r.Descripcion.ToLower().Contains(parametro.ToLower())
                   select r;
        }
        /// <summary>
        /// Filtr los recursos por el nombre la descripcion o por la sfechas de inicio o termino especificadas.
        /// </summary>
        private IQueryable<RecursoDeGrupo> BusquedaAvanzada(IQueryable<RecursoDeGrupo> recursos, ConsultaRecursoGrupoModelo opciones, string subjectId)
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

            if (opciones.EsEscritura)
            {
                var resultado = recursos.AsEnumerable().Where(r => r.EsEscritura);

                recursos = resultado.AsQueryable<RecursoDeGrupo>();
            }

            if (opciones.EsLectura)
            {
                var resultado = recursos.AsEnumerable().Where(r => r.EsLectura);

                recursos = resultado.AsQueryable<RecursoDeGrupo>();
            }

            if (opciones.EsEjecucion)
            {
                var resultado = recursos.AsEnumerable().Where(r => r.EsEjecucion);

                recursos = resultado.AsQueryable<RecursoDeGrupo>();
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
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        public Modelos.Grupo ObtenerGrupo(int id, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var gruposQuery = from rr in UnitOfWork.Set<RecursoGrupo>()
                              join ru in rolesUsuario
                                 on rr.IdRol equals ru.IdRol
                              join r in Set
                                 on rr.IdRecurso equals r.Id
                              where r.Activo && rr.EsLectura && rr.EsEscritura
                              select r;

            gruposQuery = (from r in gruposQuery
                           where r.Id == id
                           select r);

            if (gruposQuery.Count() == 0)
                return null;

            return (from r in gruposQuery
                    select new Modelos.Grupo
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion
                    }).FirstOrDefault();
        }


        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura el usuario.
        /// </summary>
        public Modelos.Grupo ObtenerGrupoPorLectura(int id, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var gruposQuery = from rr in UnitOfWork.Set<RecursoGrupo>()
                              join ru in rolesUsuario
                                 on rr.IdRol equals ru.IdRol
                              join r in Set
                                 on rr.IdRecurso equals r.Id
                              where r.Activo && rr.EsLectura
                              select r;

            gruposQuery = (from r in gruposQuery
                           where r.Id == id
                           select r);

            if (gruposQuery.Count() == 0)
                return null;

            return (from r in gruposQuery
                    select new Modelos.Grupo
                    {
                        Id = r.Id,
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem> ConsultarRoles(ConsultaRolGrupo parametros, string subjectId)
        {
            var opciones = parametros;

            //Todos los roles a los cuales poseo permisos 
            var recursosRolDeUsuarios = ObtenerRecursosDelUsuarioPor(subjectId);

            //todos los roles que estan asignados al grupo 
            var rolesGruposAdministrados = from rg in UnitOfWork.Set<Dominio.Seguridad.Entidades.RolGrupo>()
                                           where rg.IdGrupo == opciones.IdGrupo
                                           select rg;

            IQueryable<Dominio.Seguridad.Modelos.RolDeGrupoItem> query = null;

            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables. Roles que pueden ser asignados y ya son asignados
            {
                query = (from rol in recursosRolDeUsuarios
                         join rolGrupo in rolesGruposAdministrados
                             on rol.IdRecurso equals rolGrupo.IdRol
                             into at
                         from att in at.DefaultIfEmpty()
                         orderby att.FechaCreacion descending
                         select new Dominio.Seguridad.Modelos.RolDeGrupoItem()
                         {
                             Id = att == null ? 0 : att.Id,
                             IdGrupo = opciones.IdGrupo,
                             IdRol = rol.IdRecurso,
                             EsAsignado = att != null,
                             Nombre = rol.Recurso.Nombre,
                             Descripcion = rol.Recurso.Descripcion
                         });
            }
            else if ((bool)opciones.EsAsignado) //Asignados Administrables
            {

                query = from roles in recursosRolDeUsuarios
                        join rolesGrupo in rolesGruposAdministrados
                            on roles.IdRecurso equals rolesGrupo.IdRol into at
                        from att in at
                        orderby att.FechaCreacion descending
                        select new Dominio.Seguridad.Modelos.RolDeGrupoItem()
                        {
                            Id = att.Id,
                            IdGrupo = opciones.IdGrupo,
                            IdRol = roles.IdRecurso,
                            Nombre = roles.Recurso.Nombre,
                            Descripcion = roles.Recurso.Descripcion,
                            EsAsignado = true
                        };
            }
            else //Los administrables no asingnados
            {
                if (rolesGruposAdministrados.Count() <= 0)
                {
                    query = from ru in recursosRolDeUsuarios
                            select new Dominio.Seguridad.Modelos.RolDeGrupoItem()
                            {
                                Id = 0,
                                IdGrupo = opciones.IdGrupo,
                                IdRol = ru.IdRecurso,
                                EsAsignado = false,
                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion
                            };
                }
                else
                {
                    query = from ru in recursosRolDeUsuarios
                            where !rolesGruposAdministrados.Select(rra => rra.IdRol).Contains(ru.IdRecurso)
                            select new Dominio.Seguridad.Modelos.RolDeGrupoItem()
                            {
                                Id = 0,
                                IdGrupo = opciones.IdGrupo,
                                IdRol = ru.IdRecurso,
                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion,
                                EsAsignado = false
                            };
                }
            }

            if (opciones.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                query = BusquedaGenericaAccesos(query, opciones.Query);
            }
            else
            {
                if (opciones.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from g in query
                            where g.Nombre.Trim().ToLower().Contains(opciones.Nombre.Trim().ToLower())
                            select g;
                }

                if (opciones.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from g in query
                            where g.Descripcion.Trim().ToLower().Contains(opciones.Descripcion.Trim().ToLower())
                            select g;
                }
            }

            return query.Distinct().AsEnumerable().OrderByDescending(r => r.EsAsignado).Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene los usuarios del grupo filtrado por los parametros indicados.
        /// </summary>
        public ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem> ConsultarUsuarios(ConsultaUsuariosGrupo parametros, string subjectId)
        {
            var opciones = parametros;

            //todos los roles que estan asignados al grupo 
            var usuariosGrupoAdministrados = from rg in UnitOfWork.Set<Dominio.Seguridad.Entidades.UsuarioGrupo>()
                                             where rg.IdGrupo == opciones.IdGrupo
                                             select rg;

            var usuarios = from u in UnitOfWork.Set<Dominio.Seguridad.Entidades.Usuario>()
                           select u;

            IQueryable<Dominio.Seguridad.Modelos.UsuarioDeGrupoResumen> query = null;

            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables. Usuarios que pueden ser asignados y ya son asignados
            {

                query = from u in usuarios
                        join urg in usuariosGrupoAdministrados
                          on u.Id equals urg.IdUsuario
                        into at
                        from att in at.DefaultIfEmpty()
                        orderby att.FechaCreacion descending
                        select new Dominio.Seguridad.Modelos.UsuarioDeGrupoResumen()
                        {
                            Id = att == null ? 0 : att.Id,
                            IdGrupo = opciones.IdGrupo,
                            Apellidos = u.Apellidos,
                            EsAsignado = att != null,
                            IdUsuario = u.Id,
                            Nombre = u.Nombre,
                            NombreUsuario = u.NombreUsuario,
                            CorreoElectronicoLaboral = u.CorreoElectronicoLaboral,
                            CorreoElectronicoPersonal = u.CorreoElectronicoPersonal,
                            EsHabilitado = u.EsHabilitado,
                            NumeroEmpleado = u.NumeroEmpleado,
                            Area = u.AreaAdscripcion,
                            Dependencia = u.Dependencia
                        };
            }
            else if ((bool)opciones.EsAsignado)//Los asignados
            {
                var administrados = usuariosGrupoAdministrados.Select(m => m.IdUsuario);

                query = from ur in usuariosGrupoAdministrados
                        orderby ur.FechaCreacion descending
                        select new Dominio.Seguridad.Modelos.UsuarioDeGrupoResumen()
                        {
                            Id = ur.Id,
                            IdGrupo = opciones.IdGrupo,
                            IdUsuario = ur.IdUsuario,
                            Apellidos = ur.Usuario.Nombre,
                            EsAsignado = true,
                            Nombre = ur.Usuario.Nombre,
                            NombreUsuario = ur.Usuario.NombreUsuario,
                            CorreoElectronicoLaboral = ur.Usuario.CorreoElectronicoLaboral,
                            CorreoElectronicoPersonal = ur.Usuario.CorreoElectronicoPersonal,
                            EsHabilitado = ur.Usuario.EsHabilitado,
                            NumeroEmpleado = ur.Usuario.NumeroEmpleado
                        };
            }
            else //Los administrables no asingnados
            {
                var administrados = usuariosGrupoAdministrados.Select(m => m.IdUsuario);
                query = from u in usuarios
                        where !administrados.Contains(u.Id)
                        select new Dominio.Seguridad.Modelos.UsuarioDeGrupoResumen()
                        {
                            Id = 0,
                            IdGrupo = opciones.IdGrupo,
                            IdUsuario = u.Id,
                            Apellidos = u.Apellidos,
                            Nombre = u.Nombre,
                            EsAsignado = false,
                            NombreUsuario = u.NombreUsuario,
                            CorreoElectronicoLaboral = u.CorreoElectronicoLaboral,
                            CorreoElectronicoPersonal = u.CorreoElectronicoPersonal,
                            EsHabilitado = u.EsHabilitado,
                            NumeroEmpleado = u.NumeroEmpleado
                        };

            }

            if (opciones.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                var parametro = opciones.Query.Trim().ToLower();

                var filtrados = from u in UnitOfWork.Set<Dominio.Seguridad.Entidades.Usuario>()
                                where u.Nombre.Trim().ToLower().Contains(parametro)
                                     || u.Apellidos.Trim().ToLower().Contains(parametro)
                                     || u.CorreoElectronicoLaboral.Trim().ToLower().Contains(parametro)
                                     || u.CorreoElectronicoPersonal.Trim().ToLower().Contains(parametro)
                                     || u.TelefonoLaboral.Trim().ToLower().Contains(parametro)
                                     || u.NumeroEmpleado.Trim().ToLower().Contains(parametro)
                                     || u.AreaAdscripcion.Trim().ToLower().Contains(parametro)
                                     || u.Dependencia.Trim().ToLower().Contains(parametro)
                                     || u.NombreUsuario.Trim().ToLower().Contains(parametro)
                                select u.Id;

                query = from u in query
                        where filtrados.Contains(u.IdUsuario)
                        select u;
            }
            else
            {

                if (parametros.EsHabilitado != null)
                {
                    query = from u in query
                            where u.EsHabilitado == (bool)parametros.EsHabilitado
                            select u;
                }
                if (parametros.Usuario.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.NombreUsuario.Trim().ToLower().Contains(parametros.Usuario.Trim().ToLower())
                            select u;
                }
                if (parametros.Nombre.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.Nombre.Trim().ToLower().Contains(parametros.Nombre.Trim().ToLower())
                            select u;
                }
                if (parametros.Apellidos.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.Apellidos.Trim().ToLower().Contains(parametros.Apellidos.Trim().ToLower())
                            select u;
                }
                if (parametros.CorreoElectronicoPersonal.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.CorreoElectronicoPersonal.Trim().ToLower().Contains(parametros.CorreoElectronicoPersonal.Trim().ToLower())
                            select u;
                }
                if (parametros.NumeroEmpleado.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.NumeroEmpleado.Trim().ToLower().Contains(parametros.NumeroEmpleado.Trim().ToLower())
                            select u;
                }
                if (parametros.CorreoElectronicoLaboral.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.CorreoElectronicoLaboral.Trim().ToLower().Contains(parametros.CorreoElectronicoLaboral.Trim().ToLower())
                            select u;
                }

                if (parametros.Area.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.Area.Trim().ToLower().Contains(parametros.Area.Trim().ToLower())
                            select u;
                }

                if (parametros.Dependencia.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.Dependencia.Trim().ToLower().Contains(parametros.Dependencia.Trim().ToLower())
                            select u;
                }
            }

            var usuariosFinales = from u in query
                                  select new UsuarioDeGrupoItem
                                  {
                                      Id = u.Id,
                                      IdGrupo = opciones.IdGrupo,
                                      IdUsuario = u.IdUsuario,
                                      Apellidos = u.Nombre,
                                      EsAsignado = u.EsAsignado,
                                      Nombre = u.Nombre + " " + u.Apellidos,
                                      NombreUsuario = u.NombreUsuario
                                  };

            return usuariosFinales.Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene los recursos de Roles a los que tiene alcance el usuario con sus roles particulares.
        /// </summary>
        private IQueryable<RecursoRol> ObtenerRecursosDelUsuarioPor(string subjectId)
        {
            var particulares = from rp in UnitOfWork.Set<RolUsuarioVista>()
                               where rp.SubjectId == subjectId
                               select rp.IdRol;

            var recursosDeUsuario = from r in UnitOfWork.Set<RecursoRol>()
                                    join rp in particulares
                                        on r.IdRol equals rp
                                    where r.EsLectura && r.EsEjecucion && r.Recurso.Activo
                                    select r;

            return recursosDeUsuario;
        }

        private IQueryable<Dominio.Seguridad.Modelos.RolDeGrupoItem> BusquedaGenericaAccesos(IQueryable<Dominio.Seguridad.Modelos.RolDeGrupoItem> rolesGrupo, string parametro)
        {
            return from r in rolesGrupo
                   where
                       r.Nombre.ToLower().Contains(parametro.ToLower())
                       || r.Descripcion.ToLower().Contains(parametro.ToLower())
                   select r;
        }



    }
}
