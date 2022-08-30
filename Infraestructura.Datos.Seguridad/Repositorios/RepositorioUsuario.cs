using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Entidades = Dominio.Seguridad.Entidades;

namespace Infraestructura.Datos.Seguridad.Repositorios
{
    public class RepositorioUsuarios : Repository<Entidades.Usuario>, IRepositorioUsuarios
    {
        public RepositorioUsuarios(ISeguridadUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Obtiene todos los usuarios que coincidan con los prametros proporcionados.
        /// </summary>
        public ConsultaPaginada<UsuarioItem> ConsultarUsuarios(ConsultarUsuariosModelo parametros)
        {
            IQueryable<Entidades.Usuario> query = null;
            if (parametros == null)
            {
                query = from u in Set
                        select u;
            }
            else if (parametros.Query.IsNotNullOrEmptyOrWhiteSpace())//Busqueda Generica
            {
                var parametro = parametros.Query.Trim().ToLower();
                query = from u in Set
                        where u.Nombre.Trim().ToLower().Contains(parametro)
                        || u.NombreUsuario.Trim().ToLower().Contains(parametro)
                        || u.CorreoElectronicoLaboral.Trim().ToLower().Contains(parametro)
                        || u.CorreoElectronicoPersonal.Trim().ToLower().Contains(parametro)
                        || u.TelefonoLaboral.Trim().ToLower().Contains(parametro)
                        || u.NumeroEmpleado.Trim().ToLower().Contains(parametro)
                        || u.AreaAdscripcion.Trim().ToLower().Contains(parametro)
                        || u.Dependencia.Trim().ToLower().Contains(parametro)
                        select u;
            }
            else
            {
                query = from u in Set
                        select u;
                if (parametros.EsHabilitado != null)
                {
                    query = from u in Set
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
                            where u.AreaAdscripcion.Trim().ToLower().Contains(parametros.Area.Trim().ToLower())
                            select u;
                }

                if (parametros.Dependencia.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = from u in query
                            where u.Dependencia.Trim().ToLower().Contains(parametros.Dependencia.Trim().ToLower())
                            select u;
                }

                if (parametros.IdGrupo > 0)
                {
                    var grupos = from g in UnitOfWork.Set<UsuarioGrupo>()
                                 where g.IdGrupo == parametros.IdGrupo
                                 select g;

                    query = from g in grupos
                            join u in query
                                on g.IdUsuario equals u.Id
                            select u;
                }
            }

            if (query == null) return new ConsultaPaginada<UsuarioItem>()
            {
                ElementosPorPagina = parametros == null ? 20 : parametros.ElementosPorPagina,
                Pagina = parametros == null ? 1 : parametros.Pagina,
                TotalElementos = 0,
            };

            query = query.OrderByDescending(u => u.FechaCreacion);
            var result = from u in query
                         select new UsuarioItem()
                         {
                             Id = u.Id,
                             NombreUsuario = u.NombreUsuario,
                             Nombre = u.Nombre + " " + u.Apellidos,
                             EsHabilitado = u.EsHabilitado
                         };
            return result.Paginar(parametros == null ? 1 : parametros.Pagina, parametros == null ? 20 : parametros.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene el usuario con las sesiones activas.
        /// </summary>
        public Entidades.Usuario ObtenerUsuarioConSesiones(string subjectId)
        {
            var fechaActual = DateTime.Now;
            var usuarioConSesiones = (from u in Set
                                      where u.SubjectId == subjectId
                                      select new
                                      {
                                          usuario = u,
                                          sesiones = u.Sesiones.Where(s => s.Expira > fechaActual)
                                      });

            var usr = usuarioConSesiones.ToList().Select(t => t.usuario).FirstOrDefault();
            return usr;
        }

        /// <summary>
        /// Agrega una sesión de usuario.
        /// </summary>
        public void AgregarSesion(Sesion sesion)
        {
            if (sesion == null) return;

            UnitOfWork.Set<Sesion>().Add(sesion);
        }

        /// <summary>
        /// Obtiene la sesion indicada
        /// </summary>
        public Sesion ObtenerSesion(int idSesion)
        {
            return (from s in UnitOfWork.Set<Sesion>()
                    where s.Id == idSesion
                    select s).FirstOrDefault();
        }

        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        public List<Sesion> ObtenerSesionesActivas(string subjectId)
        {
            var fechaActual = DateTime.Now;
            return (from s in UnitOfWork.Set<Sesion>()
                    where s.Usuario.SubjectId == subjectId
                    && s.Expira > fechaActual
                    select s).ToList();
        }

        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        public List<Sesion> ObtenerSesionesActivas(int idUsuario)
        {
            var fechaActual = DateTime.Now;
            return (from s in UnitOfWork.Set<Sesion>()
                    where s.Usuario.Id == idUsuario
                    && s.Expira > fechaActual
                    select s).ToList();
        }

        /// <summary>
        /// Actualiza una sesión.
        /// </summary>
        public void ActualizarSesion(Sesion sesion)
        {
            UnitOfWork.Set<Sesion>().Update(sesion);
        }

        /// <summary>
        /// Permite agregar y remover los roles para el usuario.
        /// </summary>
        public void AdministrarRoles(IEnumerable<RolUsuario> paraAgregar, IEnumerable<RolUsuario> paraEliminar)
        {
            var rolesUsuario = UnitOfWork.Set<RolUsuario>();
            if (paraAgregar != null && paraAgregar.Count() > 0)
            {
                rolesUsuario.AddRange(paraAgregar);
            }
            if (paraEliminar != null && paraEliminar.Count() > 0)
            {
                rolesUsuario.RemoveRange(paraEliminar);
            }
        }

        /// <summary>
        /// Obtiene un contador de los roles directos del usuario indicado.
        /// Nota el IdUsuario es el subjectId.
        /// </summary>
        public UsuarioRolesDirectosContador ContarRolesDirectos(int idUsuario)
        {
            var usuario = (from u in Set
                           join rd in UnitOfWork.Set<RolDirectoVista>()
                                on u.SubjectId equals rd.SubjectId
                           where u.Id == idUsuario
                           select rd).GroupBy(rd => rd.SubjectId).Select(gu => new UsuarioRolesDirectosContador() { SubjectId = gu.Key, Roles = gu.Count() }).FirstOrDefault();
            if (usuario == null)
            {
                return (from u in Set
                        where u.Id == idUsuario
                        select new UsuarioRolesDirectosContador() { IdUsuario = u.Id, SubjectId = u.SubjectId, Roles = 0 }).FirstOrDefault();
            }

            return usuario;
        }

        /// <summary>
        /// Agrega roles a la vista de roles directos.
        /// </summary>
        public void AgregarRolesDirectos(IEnumerable<RolDirectoVista> rolUsuarioVistas)
        {
            if (rolUsuarioVistas == null || rolUsuarioVistas.Count() <= 0) return;

            UnitOfWork.Set<RolDirectoVista>().AddRange(rolUsuarioVistas);
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
        /// Iguala los roles particulares con los roles de usuario omitiendo los roles eliminado.
        /// </summary>
        public void IgualarRolesParticulares(IEnumerable<int> rolesEliminados, string subjectId)
        {
            var rolesUsuario = (from ru in UnitOfWork.Set<RolUsuarioVista>()
                                where !rolesEliminados.Contains(ru.IdRol)
                                select new RolParticularVista()
                                {
                                    IdRol = ru.IdRol,
                                    SubjectId = ru.SubjectId
                                }).ToList();

            var rolesParticulares = (from ru in UnitOfWork.Set<RolParticularVista>()
                                     where ru.SubjectId == subjectId
                                     select ru).ToList();

            if (rolesParticulares != null && rolesParticulares.Count > 0)
            {
                UnitOfWork.Set<RolParticularVista>().RemoveRange(rolesParticulares);
            }

            if (rolesUsuario != null && rolesUsuario.Count > 0)
            {
                UnitOfWork.Set<RolParticularVista>().AddRange(rolesUsuario);
            }
        }
        /// <summary>
        /// Remueve roles a la vista de roles directos.
        /// </summary>
        public void RemoverRolesDirectos(IEnumerable<RolDirectoVista> rolDirectoVistas)
        {
            if (rolDirectoVistas == null || rolDirectoVistas.Count() <= 0) return;

            UnitOfWork.Set<RolDirectoVista>().RemoveRange(rolDirectoVistas);
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
        public void RemoverRolesParticulares(string subjectId)
        {
            var roles = UnitOfWork.Set<RolParticularVista>().Where(r => r.SubjectId == subjectId).ToList();
            if (roles != null && roles.Count > 0)
            {
                UnitOfWork.Set<RolParticularVista>().RemoveRange(roles);
            }
        }
        /// <summary>
        /// Obtiene una lista de roles de usuario el usuario indicado
        /// </summary>
        public List<RolVistaBase> ObtenerRolesUsuario(string idUsuario)
        {
            return (from ru in UnitOfWork.Set<RolUsuarioVista>()
                    where ru.SubjectId == idUsuario
                    select new RolVistaBase()
                    {
                        Id = ru.Id,
                        IdRol = ru.IdRol,
                        SubjectId = ru.SubjectId
                    }).ToList();
        }

        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los grupos que seran eliminados.
        /// </summary>
        public Respuesta CompilarRolesPorGrupos(IEnumerable<int> removerGrupos)
        {//Nota: Este metodo sera usado al eliminar uno o mas grupos.
            var usuariosParaCompilar = from g in UnitOfWork.Set<UsuarioGrupo>()
                                       join u in Set
                                           on g.IdUsuario equals u.Id
                                       where removerGrupos.Contains(g.IdGrupo)//Elimina los grupos que se estan quitando
                                       select new UsuarioConRoles()
                                       {
                                           IdUsuario = u.Id,
                                           SubjectId = u.SubjectId,
                                           RolesDirectos = u.Roles.Select(r => r.IdRol),
                                           RolesGrupos = u.Grupos.Where(g => !removerGrupos.Contains(g.IdGrupo))
                                                                 .SelectMany(g => g.Grupo.Roles.Select(r => r.IdRol))
                                       };

            CompilarGruposRoles(usuariosParaCompilar);
            return new Respuesta();
        }
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que seran eliminados.
        /// </summary>
        public Respuesta CompilarRolesPorRoles(IEnumerable<int> removerRoles)
        {//Nota: Este metodo sera usado al eliminar uno o mas roles.
            var usuariosParaCompilar = from ru in UnitOfWork.Set<RolUsuarioVista>()
                                       join u in Set
                                           on ru.SubjectId equals u.SubjectId
                                       where removerRoles.Contains(ru.IdRol)
                                       select new UsuarioConRoles()
                                       {
                                           IdUsuario = u.Id,
                                           SubjectId = u.SubjectId,
                                           RolesDirectos = u.Roles.Where(rd => !removerRoles.Contains(rd.IdRol)).Select(r => r.IdRol),
                                           RolesGrupos = u.Grupos.SelectMany(g => g.Grupo.Roles.Where(rg => !removerRoles.Contains(rg.IdRol)).Select(r => r.IdRol))
                                       };

            CompilarGruposRoles(usuariosParaCompilar);
            return new Respuesta();
        }
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que asignados o removidos de la lista de roles directos del usuario.
        /// </summary>
        public Respuesta CompilarRolesPorUsuario(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idUsuario)
        {//Nota: Este metodo sera usado al administrar los roles de usuario.
            var usuariosParaCompilar = (from u in Set
                                        where u.Id == idUsuario
                                        select new UsuarioConRoles()
                                        {
                                            IdUsuario = u.Id,
                                            SubjectId = u.SubjectId,
                                            RolesDirectos = u.Roles.Where(rd => !removerRoles.Contains(rd.IdRol)).Select(r => r.IdRol).ToList(),
                                            RolesGrupos = u.Grupos.SelectMany(g => g.Grupo.Roles.Select(r => r.IdRol)).ToList()
                                        }).ToList();
            if (usuariosParaCompilar != null)
            {
                foreach (var u in usuariosParaCompilar)
                {
                    var rd = u.RolesDirectos.ToList();
                    rd.AddRange(nuevosRoles);
                    u.RolesDirectos = rd;
                }
            }

            CompilarGruposRoles(usuariosParaCompilar);

            return new Respuesta();
        }
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los usuarios asignados o removidos del grupo indicado.
        /// </summary>
        public Respuesta CompilarRolesPorAsignacionUsuarios(IEnumerable<int> nuevosUsuarios, IEnumerable<int> removerUsuarios, int idGrupo)
        {//Nota: Este metodo sera usado al administrar los usuarios de un grupo.
            var usuariosRemovidosDelGrupo = from u in Set
                                            where removerUsuarios.Contains(u.Id)
                                            select new UsuarioConRoles()
                                            {
                                                IdUsuario = u.Id,
                                                SubjectId = u.SubjectId,
                                                RolesDirectos = u.Roles.Select(r => r.IdRol),
                                                RolesGrupos = u.Grupos.Where(g => g.IdGrupo != idGrupo).SelectMany(g => g.Grupo.Roles.Select(r => r.IdRol))
                                            };

            var nuevosUsuariosDelGrupo = (from u in Set
                                          where nuevosUsuarios.Contains(u.Id)
                                          select new UsuarioConRoles()
                                          {
                                              IdUsuario = u.Id,
                                              SubjectId = u.SubjectId,
                                              RolesDirectos = u.Roles.Select(r => r.IdRol),
                                              RolesGrupos = u.Grupos.SelectMany(g => g.Grupo.Roles.Select(r => r.IdRol))
                                          }).ToList();

            var rolesDelGrupo = from rg in UnitOfWork.Set<RolGrupo>()
                                where rg.IdGrupo == idGrupo
                                select rg.IdRol;

            if (nuevosUsuariosDelGrupo != nuevosUsuarios)
            {
                foreach (var usr in nuevosUsuariosDelGrupo)
                {
                    var rolesGrupo = usr.RolesGrupos.ToList();
                    rolesGrupo.AddRange(rolesDelGrupo);
                    usr.RolesGrupos = rolesGrupo;
                }
            }

            var usuariosParaCompilar = usuariosRemovidosDelGrupo.ToList();
            usuariosParaCompilar.AddRange(nuevosUsuariosDelGrupo);

            CompilarGruposRoles(usuariosParaCompilar);
            return new Respuesta();
        }
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles asignados o removidos del grupo indicado.
        /// </summary>
        public Respuesta CompilarRolesPorAsignacionRoles(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idGrupo)
        {//Nota : Este metodo sera usado al agregar o quitar roles de un grupo.
            var usuariosParaCompilar = (from u in Set
                                        where u.Grupos.Any(g => g.IdGrupo == idGrupo)
                                        select new UsuarioConRoles()
                                        {
                                            IdUsuario = u.Id,
                                            SubjectId = u.SubjectId,
                                            RolesDirectos = u.Roles.Select(r => r.IdRol),
                                            RolesGrupos = u.Grupos.SelectMany(g => g.Grupo.Roles
                                                                                          .Where(rg => (rg.IdGrupo != idGrupo) || (rg.IdGrupo == idGrupo && !removerRoles.Contains(rg.IdRol)))
                                                                                          .Select(r => r.IdRol))
                                        }).ToList();

            if (usuariosParaCompilar != null)
            {
                foreach (var usuario in usuariosParaCompilar)
                {
                    var rolesGrupo = usuario.RolesGrupos.ToList();
                    rolesGrupo.AddRange(nuevosRoles);
                    usuario.RolesGrupos = rolesGrupo;
                }
            }

            CompilarGruposRoles(usuariosParaCompilar);

            return new Respuesta();
        }

        private void CompilarGruposRoles(IEnumerable<UsuarioConRoles> usuariosParaCompilar)
        {
            if (usuariosParaCompilar == null || usuariosParaCompilar.Count() <= 0) return;

            usuariosParaCompilar = usuariosParaCompilar.AsQueryable().Distinct();

            var rolesDirectos = new List<RolDirectoVista>();
            var rolesParticulares = new List<RolParticularVista>();
            var RolesUsuario = new List<RolUsuarioVista>();

            foreach (var usuario in usuariosParaCompilar)
            {
                if (usuario.RolesDirectos != null && usuario.RolesDirectos.Count() > 0)
                {
                    foreach (var rolDirecto in usuario.RolesDirectos)
                    {
                        var item = new RolDirectoVista() { IdRol = rolDirecto, SubjectId = usuario.SubjectId };
                        rolesDirectos.Add(item);

                        var itemParticular = new RolParticularVista() { IdRol = rolDirecto, SubjectId = usuario.SubjectId };
                        rolesParticulares.Add(itemParticular);

                        var itemUsuario = new RolUsuarioVista() { IdRol = rolDirecto, SubjectId = usuario.SubjectId };
                        RolesUsuario.Add(itemUsuario);
                    }
                }

                if (usuario.RolesGrupos != null && usuario.RolesGrupos.Count() > 0)
                {
                    foreach (var rolGrupo in usuario.RolesGrupos)
                    {
                        var itemUsuario = new RolUsuarioVista() { IdRol = rolGrupo, SubjectId = usuario.SubjectId };
                        RolesUsuario.Add(itemUsuario);

                        if (!usuario.RolesDirectos.Any())
                        {
                            var itemParticular = new RolParticularVista() { IdRol = rolGrupo, SubjectId = usuario.SubjectId };
                            rolesParticulares.Add(itemParticular);
                        }
                    }
                }

            }


            var rolesDirectosViejos = from rd in UnitOfWork.Set<RolDirectoVista>()
                                      where usuariosParaCompilar.Select(u => u.SubjectId).Contains(rd.SubjectId)
                                      select rd;

            var rolesParticularesViejos = from rp in UnitOfWork.Set<RolParticularVista>()
                                          where usuariosParaCompilar.Select(u => u.SubjectId).Contains(rp.SubjectId)
                                          select rp;

            var rolesUsuarioViejos = from ru in UnitOfWork.Set<RolUsuarioVista>()
                                     where usuariosParaCompilar.Select(u => u.SubjectId).Contains(ru.SubjectId)
                                     select ru;

            UnitOfWork.Set<RolDirectoVista>().AddRange(rolesDirectos);
            UnitOfWork.Set<RolDirectoVista>().RemoveRange(rolesDirectosViejos);

            UnitOfWork.Set<RolParticularVista>().AddRange(rolesParticulares);
            UnitOfWork.Set<RolParticularVista>().RemoveRange(rolesParticularesViejos);

            UnitOfWork.Set<RolUsuarioVista>().AddRange(RolesUsuario);
            UnitOfWork.Set<RolUsuarioVista>().RemoveRange(rolesUsuarioViejos);

            //UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase> ConsultarRoles(ConsultaRol parametros, string subjectId)
        {
            var opciones = parametros;

            var recursosAccionDeUsuarios = ObtenerRecursosDelUsuarioPor(subjectId);

            var rolesDeRolAdministrado = from rc in UnitOfWork.Set<Dominio.Seguridad.Entidades.RolUsuario>()
                                         where rc.IdUsuario == opciones.IdUsuario && recursosAccionDeUsuarios.Select(r => r.IdRecurso).Contains(rc.IdRol)
                                         select rc;

            IQueryable<Dominio.Seguridad.Modelos.RolDeUsuarioBase> query = null;

            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables.
            {
                query = (from ru in recursosAccionDeUsuarios
                         join ar in rolesDeRolAdministrado
                             on ru.IdRecurso equals ar.IdRol into at
                         from att in at.DefaultIfEmpty()
                         orderby att.FechaCreacion descending
                         select new Dominio.Seguridad.Modelos.RolDeUsuarioBase()
                         {
                             Id = att == null ? 0 : att.Id,
                             IdUsuario = opciones.IdUsuario,
                             IdRol = ru.IdRecurso,
                             EsAsignado = att != null ? true : false,
                             Nombre = ru.Recurso.Nombre,
                             Descripcion = ru.Recurso.Descripcion
                         });
            }
            else if ((bool)opciones.EsAsignado) //Asignados Administrables
            {

                query = (from ru in recursosAccionDeUsuarios
                         join ar in rolesDeRolAdministrado
                             on ru.IdRecurso equals ar.IdRol into at
                         from att in at
                         where att.IdUsuario == opciones.IdUsuario
                         orderby att.FechaCreacion descending
                         select new Dominio.Seguridad.Modelos.RolDeUsuarioBase()
                         {
                             Id = att.Id,
                             IdUsuario = opciones.IdUsuario,
                             IdRol = ru.IdRecurso,
                             EsAsignado = true,
                             Nombre = ru.Recurso.Nombre,
                             Descripcion = ru.Recurso.Descripcion
                         });
            }
            else //Los administrables no asingnados
            {
                if (rolesDeRolAdministrado.Count() <= 0)
                {
                    query = from ru in recursosAccionDeUsuarios
                            select new Dominio.Seguridad.Modelos.RolDeUsuarioBase()
                            {
                                Id = 0,
                                IdUsuario = opciones.IdUsuario,
                                IdRol = ru.IdRecurso,
                                EsAsignado = false,
                                Nombre = ru.Recurso.Nombre,
                                Descripcion = ru.Recurso.Descripcion
                            };
                }
                else
                {
                    query = (from ru in recursosAccionDeUsuarios
                             where !rolesDeRolAdministrado.Select(rra => rra.IdRol).Contains(ru.IdRecurso)
                             select new Dominio.Seguridad.Modelos.RolDeUsuarioBase()
                             {
                                 Id = 0,
                                 IdUsuario = opciones.IdUsuario,
                                 IdRol = ru.IdRecurso,
                                 EsAsignado = false,
                                 Nombre = ru.Recurso.Nombre,
                                 Descripcion = ru.Recurso.Descripcion
                             });
                }
            }

            if (opciones.Query.IsNotNullOrEmptyOrWhiteSpace())
            {
                query = BusquedaGenericaAccesos(query, opciones.Query);
            }
            else
            {
                query = BusquedaAvanzadaAccesos(query, opciones);
            }

            return query.Distinct().AsEnumerable().OrderByDescending(r => r.EsAsignado).Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        private IQueryable<Dominio.Seguridad.Modelos.RolDeUsuarioBase> BusquedaGenericaAccesos(IQueryable<Dominio.Seguridad.Modelos.RolDeUsuarioBase> roles, string parametro)
        {
            return from r in roles
                   where
                       r.Nombre.ToLower().Contains(parametro.ToLower())
                       || r.Descripcion.ToLower().Contains(parametro.ToLower())
                   select r;
        }

        private IQueryable<Dominio.Seguridad.Modelos.RolDeUsuarioBase> BusquedaAvanzadaAccesos(IQueryable<Dominio.Seguridad.Modelos.RolDeUsuarioBase> roles, ConsultaRol opciones)
        {
            if (opciones.Nombre.IsNotNullOrEmptyOrWhiteSpace())
            {
                roles = from r in roles
                        where r.Nombre.ToLower().Contains(opciones.Nombre.ToLower())
                        select r;
            }
            if (opciones.Descripcion.IsNotNullOrEmptyOrWhiteSpace())
            {
                roles = from r in roles
                        where r.Descripcion.ToLower().Contains(opciones.Descripcion.ToLower())
                        select r;
            }

            return roles;
        }


        private IQueryable<RecursoRol> ObtenerRecursosDelUsuarioPor(string subjectId)
        {
            var particulares = from rp in UnitOfWork.Set<RolParticularVista>()// rol usuario vista 
                               where rp.SubjectId == subjectId
                               select rp.IdRol;

            var recursosDeUsuario = from r in UnitOfWork.Set<RecursoRol>()
                                    join rp in particulares
                                        on r.IdRol equals rp
                                    where r.EsLectura && r.EsEjecucion && r.Recurso.Activo
                                    select r;

            return recursosDeUsuario;
        }

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos
        /// </summary>
        public Dictionary<string, string> ObtenerNombreUsuarios(List<string> subjectIds)
        {
            if (subjectIds is null || !subjectIds.Any())
                return new Dictionary<string, string>();

            var resultado = (from u in Set
                             where subjectIds.Contains(u.SubjectId)
                             select u).ToDictionary(r => r.SubjectId, r => r.Nombre + " " + r.Apellidos);

            return resultado;
        }

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos por ID
        /// </summary>
        public Dictionary<int, string> ObtenerNombreUsuarios(List<int> ids)
        {
            if (ids is null || !ids.Any())
                return new Dictionary<int, string>();

            var resultado = (from u in Set
                             where ids.Contains(u.Id)
                             select u).ToDictionary(r => r.Id, r => r.Nombre + " " + r.Apellidos);

            return resultado;
        }

        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        public List<UsuarioItem> ObtenerUsuarios()
        {
            var query = (from u in Set
                         where u.EsHabilitado
                         select new UsuarioItem
                         {
                             Id = u.Id,
                             EsHabilitado = u.EsHabilitado,
                             Nombre = $"{u.Nombre} {u.Apellidos}",
                             NombreUsuario = u.NombreUsuario
                         }).ToList();

            return query;
        }
        /// <summary>
        /// Metodo que obtiene los usuarios que tienen algun rol de una lista de roles
        /// </summary>
        /// <param name="idsRoles"></param>
        /// <returns></returns>
        public List<UsuarioNotificacion> ObtenerUsuariosporRoles(List<int> idsRoles)
        {
            var query = (from u in Set
                         join ru in UnitOfWork.Set<Entidades.RolUsuario>() on new { IdUsuario = u.Id, rolSeleccionado = true } equals new { ru.IdUsuario, rolSeleccionado = (idsRoles.Contains(ru.IdRol)) }
                         where u.EsHabilitado
                         select new UsuarioNotificacion
                         {
                             Id = u.Id,
                             NombreCompleto = u.Nombre + u.Apellidos,
                             CorreoElectronicoLaboral = u.CorreoElectronicoLaboral,
                             CorreoElectronicoPersonal = u.CorreoElectronicoPersonal
                         }).ToList();

            return query;
        }

        /// <summary>
        /// Retorna el listado de usuarios con funcionalidad de busqueda por criterio
        /// Utilizado en autocomplete de vistas
        /// </summary>
        public List<UsuarioItem> ObtenerUsuariosAutocomplete(ConsultarUsuariosModelo consulta)
        {
            if (consulta.Query.IsNullOrEmptyOrWhiteSpace())
            {
                return new List<UsuarioItem>();
            }


            var query = from u in Set
                        let nombreCompleto = (u.Nombre ?? string.Empty) + " " + (u.Apellidos ?? string.Empty)
                        where nombreCompleto.Trim().ToLower().Contains((consulta.Query ?? string.Empty).Trim().ToLower())
                        && u.EsHabilitado
                        select u;

            return query.Select(u => new UsuarioItem()
            {
                EsHabilitado = u.EsHabilitado,
                Id = u.Id,
                Nombre = u.Nombre + " " + u.Apellidos,
                NombreUsuario = u.NombreUsuario
            }).ToList();
        }
    }



    internal class UsuarioConRoles
    {
        public int IdUsuario { get; set; }
        public string SubjectId { get; set; }
        public IEnumerable<int> RolesDirectos { get; set; }
        public IEnumerable<int> RolesGrupos { get; set; }
    }
}
