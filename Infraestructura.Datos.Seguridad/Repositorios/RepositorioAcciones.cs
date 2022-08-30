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

namespace Infraestructura.Datos.Seguridad.Repositorios
{
    public class RepositorioAcciones : RepositorioRecurso<Dominio.Seguridad.Entidades.Accion, RecursoAccion>, IRepositorioAcciones
    {
        public RepositorioAcciones(ISeguridadUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public override ConsultaPaginada<IPermisoModel> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (ConsultaRecursoAccionModelo)parametros;

            var recursosDeUsuario = ObtenerRecursosDelUsuarioPor(subjectId);

            var recursosDelRolAdministrado = from r in UnitOfWork.Set<RecursoAccion>()
                                             where r.IdRol == opciones.IdRol && r.Recurso.Activo
                                             select r;

            IQueryable<RecursoDeAccion> query = null;
            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables.
            {
                query = from ru in recursosDeUsuario
                        join rr in recursosDelRolAdministrado
                            on ru.IdRecurso equals rr.IdRecurso into rt
                        from rtt in rt.DefaultIfEmpty()
                        select new RecursoDeAccion()
                        {
                            Id = rtt == null ? 0 : rtt.Id,
                            IdRol = opciones.IdRol,
                            IdRecurso = ru.IdRecurso,

                            EsLectura = rtt == null ? false : rtt.EsLectura,
                            EsEscritura = rtt == null ? false : rtt.EsEscritura,
                            EsEjecucion = rtt == null ? false : rtt.EsEjecucion,

                            FechaAsignacion = rtt == null ? (DateTime?)null : rtt.FechaCreacion,

                            Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                            Opcion = ru.Recurso.OpcionModulo.Nombre,
                            Accion = ru.Recurso.Nombre,
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
                            select new RecursoDeAccion()
                            {
                                Id = rrAdmin.Id,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = rrAdmin.EsLectura,
                                EsEscritura = rrAdmin.EsEscritura,
                                EsEjecucion = rrAdmin.EsEjecucion,

                                FechaAsignacion = rrAdmin.FechaCreacion,

                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
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
                            select new RecursoDeAccion()
                            {
                                Id = rrAdmin.Id,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = rrAdmin.EsLectura,
                                EsEscritura = rrAdmin.EsEscritura,
                                EsEjecucion = rrAdmin.EsEjecucion,

                                FechaAsignacion = rrAdmin.FechaCreacion,

                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
                                EsAsignado = true
                            };
                }
            }
            else //Los administrables no asingnados
            {
                if (recursosDelRolAdministrado.Count() <= 0)
                {
                    query = from ru in recursosDeUsuario
                            select new RecursoDeAccion()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = false,
                                EsEscritura = false,
                                EsEjecucion = false,

                                FechaAsignacion = (DateTime?)null,

                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
                                EsAsignado = false
                            };
                }
                else
                {
                    query = from ru in recursosDeUsuario
                            where !recursosDelRolAdministrado.Select(rra => rra.IdRecurso).Contains(ru.IdRecurso)
                            select new RecursoDeAccion()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdRecurso = ru.IdRecurso,

                                EsLectura = false,
                                EsEscritura = false,
                                EsEjecucion = false,

                                FechaAsignacion = (DateTime?)null,

                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
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

            var resultado = query.AsEnumerable().OrderByDescending(r => r.EsAsignado).ThenBy(r => r.FechaAsignacion);

            return resultado.AsQueryable<IPermisoModel>().Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        public ConsultaPaginada<Dominio.Seguridad.Modelos.Acceso> ConsultarAccesos(ConsultaAcceso parametros, string subjectId)
        {
            var opciones = parametros;

            var recursosAccionDeUsuarios = ObtenerRecursosDelUsuarioPor(subjectId);

            var accesosDeRolAdministrado = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                                           where acc.IdRol == opciones.IdRol && acc.Activo
                                           select acc;

            var x = accesosDeRolAdministrado.ToList();

            IQueryable<Dominio.Seguridad.Modelos.Acceso> query = null;

            if (opciones.EsAsignado == null) // Asignados admininstrables y asignables.
            {
                query = from ru in recursosAccionDeUsuarios
                        join ar in accesosDeRolAdministrado
                            on ru.IdRecurso equals ar.IdAccion into at
                        from att in at.DefaultIfEmpty()
                        orderby att.FechaModificacion descending
                        select new Dominio.Seguridad.Modelos.Acceso()
                        {
                            Id = att == null ? 0 : att.Id,
                            IdRol = opciones.IdRol,
                            IdAccion = ru.IdRecurso,
                            Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                            Opcion = ru.Recurso.OpcionModulo.Nombre,
                            Accion = ru.Recurso.Nombre,
                            FechaCaducidad = att == null ? (DateTime?)null : att.FechaCaducidad,
                            EsAsignado = att != null
                        };

            }
            else if ((bool)opciones.EsAsignado) //Asignados Administrables
            {

                query = from ru in recursosAccionDeUsuarios
                        join ar in accesosDeRolAdministrado
                            on ru.IdRecurso equals ar.IdAccion into at
                        from att in at
                        where att.IdRol == opciones.IdRol
                        orderby att.FechaModificacion descending
                        select new Dominio.Seguridad.Modelos.Acceso()
                        {
                            Id = att.Id,
                            IdRol = opciones.IdRol,
                            IdAccion = ru.IdRecurso,
                            Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                            Opcion = ru.Recurso.OpcionModulo.Nombre,
                            Accion = ru.Recurso.Nombre,
                            FechaCaducidad = att.FechaCaducidad,
                            EsAsignado = true
                        };
            }
            else //Los administrables no asingnados
            {
                if (accesosDeRolAdministrado.Count() <= 0)
                {
                    query = from ru in recursosAccionDeUsuarios
                            select new Dominio.Seguridad.Modelos.Acceso()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdAccion = ru.IdRecurso,
                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
                                FechaCaducidad = null,
                                EsAsignado = false
                            };
                }
                else
                {
                    query = from ru in recursosAccionDeUsuarios
                            where !accesosDeRolAdministrado.Select(rra => rra.IdAccion).Contains(ru.IdRecurso)
                            select new Dominio.Seguridad.Modelos.Acceso()
                            {
                                Id = 0,
                                IdRol = opciones.IdRol,
                                IdAccion = ru.IdRecurso,
                                Modulo = ru.Recurso.OpcionModulo.Modulo.Nombre,
                                Opcion = ru.Recurso.OpcionModulo.Nombre,
                                Accion = ru.Recurso.Nombre,
                                FechaCaducidad = null,
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
                query = BusquedaAvanzadaAccesos(query, opciones);
            }

            var filtrar = query.Distinct();

            return filtrar.AsEnumerable().OrderByDescending(a => a.EsAsignado).ThenBy(a => a.FechaCaducidad).Paginar(opciones.Pagina, opciones.ElementosPorPagina);
        }

        /// <summary>
        /// Obtiene un acceso hacia la accion al cual el usuario tenga permiso.
        /// </summary>
        public Dominio.Seguridad.Entidades.Acceso ObtenerAcceso(string accion, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru.IdRol;

            var fecha = DateTime.Now.StartDay();

            var accesos = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                          join ru in rolesUsuario
                              on acc.IdRol equals ru
                          where acc.Activo && acc.FechaCaducidad >= fecha
                                && acc.Accion.Ruta == accion && acc.Accion.Activo
                          select acc;

            return accesos.FirstOrDefault();
        }

        public List<Dominio.Seguridad.Entidades.Acceso> ObtenerAccesos(List<string> acciones, string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru.IdRol;

            var fecha = DateTime.Now.StartDay();

            var accionesBusqueda = acciones.Select(ac => ac.ToLower()).ToList();

            var accesos = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                          join ru in rolesUsuario
                              on acc.IdRol equals ru
                          where acc.Activo && acc.FechaCaducidad >= fecha
                                && accionesBusqueda.Contains(acc.Accion.Ruta.ToLower()) && acc.Accion.Activo
                          select new
                          {
                              acceso = acc,
                              accion = acc.Accion
                          };

            return accesos.ToList().Select(ac => ac.acceso).ToList();
        }

        /// <summary>
        /// Obtiene un acceso al cual el usuario tenga permiso.
        /// </summary>
        public Dominio.Seguridad.Entidades.Acceso ObtenerAcceso(int IdAcceso, string subjectId)
        {
            /*var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru.IdRol;

            var fecha = DateTime.Now.StartDay();

            var accesos = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                          join ru in rolesUsuario
                              on acc.IdRol equals ru
                          where acc.Activo && acc.FechaCaducidad >= fecha
                                && acc.Id == IdAcceso && acc.Activo
                          select acc;*/


            var recursosAccionDeUsuarios = ObtenerRecursosDelUsuarioPor(subjectId);

            var accesosDeRolAdministrado = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                                           where acc.Id == IdAcceso
                                           select acc;

            var accesos = from ru in recursosAccionDeUsuarios
                          join ar in accesosDeRolAdministrado
                              on ru.IdRecurso equals ar.IdAccion
                          where ar.Id == IdAcceso
                          select ar;

            return accesos.FirstOrDefault();
        }

        /// <summary>
        /// Obtiene los modulos con con opciones de modulo y acciones principales a las que puede acceder un usuario
        /// </summary>
        public List<Dominio.Seguridad.Modelos.Modulo> ObtenerModulos(string subjectId)
        {
            var rolesUsuario = from ru in UnitOfWork.Set<RolUsuarioVista>()
                               where ru.SubjectId == subjectId
                               select ru;

            var fecha = DateTime.Now.StartDay();

            var accesos = from acc in UnitOfWork.Set<Dominio.Seguridad.Entidades.Acceso>()
                          join ru in rolesUsuario
                              on acc.IdRol equals ru.IdRol
                          where acc.Activo && acc.FechaCaducidad >= fecha
                          select acc;

            var acciones = from ac in Set
                           join acc in accesos
                           on ac.Id equals acc.IdAccion
                           where ac.Activo
                           select ac;

            var opciones = (from op in UnitOfWork.Set<Dominio.Seguridad.Entidades.OpcionModulo>()
                            join ac in acciones
                            on op.Id equals ac.IdOpcionModulo
                            where op.Activo
                            select op).Distinct();

            var modulos = (from m in UnitOfWork.Set<Dominio.Seguridad.Entidades.Modulo>()
                           join op in opciones
                           on m.Id equals op.IdModulo
                           where m.Activo
                           select new
                           {
                               modulo = m,
                               opciones = m.Opciones.Where(op => op.Activo && opciones.Contains(op)),
                               acciones = m.Opciones.Where(op => op.Activo).SelectMany(op => op.Acciones).Where(ac => ac.Activo && ac.EsPrincipal)
                           }).ToList().Select(m => m.modulo).Distinct().ToList();

            var moduloModelo = new List<Dominio.Seguridad.Modelos.Modulo>();

            foreach (var item in modulos)
            {
                moduloModelo.Add(item.ToModel<Dominio.Seguridad.Modelos.Modulo>());

            }

            //var x = modulos.ToList().Select(m => m.modulo.ToModel<Dominio.Seguridad.Modelos.Modulo>()).ToList();

            return moduloModelo;
        }

        /// <summary>
        /// Obtiene los recursos de grupos a los que tiene alcance el usuario con sus roles particulares.
        /// </summary>
        private IQueryable<RecursoAccion> ObtenerRecursosDelUsuarioPor(string subjectId)
        {
            var particulares = from rp in UnitOfWork.Set<RolParticularVista>()
                               where rp.SubjectId == subjectId
                               select rp.IdRol;

            var recursosDeUsuario = (from r in UnitOfWork.Set<RecursoAccion>()
                                     join rp in particulares
                                         on r.IdRol equals rp
                                     where r.EsLectura && r.Recurso.Activo
                                     select r).Distinct();

            return recursosDeUsuario;
        }
        /// <summary>
        /// Filtra los recursos donde el nombre de la acción, la opción o el modulo cohincida con el parametro Query
        /// </summary>
        private IQueryable<RecursoDeAccion> BusquedaGenerica(IQueryable<RecursoDeAccion> recursos, string parametro)
        {
            return from r in recursos
                   where
                       r.Accion.ToLower().Contains(parametro.ToLower())
                       || r.Opcion.ToLower().Contains(parametro.ToLower())
                       || r.Modulo.ToLower().Contains(parametro.ToLower())
                   select r;
        }
        /// <summary>
        /// Filtra los recursos por la accion, la opción, el modulo y las fechas de inicio o termino especificadas.
        /// </summary>
        private IQueryable<RecursoDeAccion> BusquedaAvanzada(IQueryable<RecursoDeAccion> recursos, ConsultaRecursoAccionModelo opciones)
        {
            if (opciones.Accion.IsNotNullOrEmptyOrWhiteSpace())
            {
                recursos = from r in recursos
                           where r.Accion.ToLower().Contains(opciones.Accion.ToLower())
                           select r;
            }
            if (opciones.Opcion.IsNotNullOrEmptyOrWhiteSpace())
            {
                recursos = from r in recursos
                           where r.Opcion.ToLower().Contains(opciones.Opcion.ToLower())
                           select r;
            }
            if (opciones.Modulo.IsNotNullOrEmptyOrWhiteSpace())
            {
                recursos = from r in recursos
                           where r.Modulo.ToLower().Contains(opciones.Modulo.ToLower())
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


        private IQueryable<Dominio.Seguridad.Modelos.Acceso> BusquedaGenericaAccesos(IQueryable<Dominio.Seguridad.Modelos.Acceso> accesos, string parametro)
        {
            return from r in accesos
                   where
                       r.Accion.ToLower().Contains(parametro.ToLower())
                       || r.Opcion.ToLower().Contains(parametro.ToLower())
                       || r.Modulo.ToLower().Contains(parametro.ToLower())
                   select r;
        }

        private IQueryable<Dominio.Seguridad.Modelos.Acceso> BusquedaAvanzadaAccesos(IQueryable<Dominio.Seguridad.Modelos.Acceso> accesos, ConsultaAcceso opciones)
        {
            if (opciones.Accion.IsNotNullOrEmptyOrWhiteSpace())
            {
                accesos = from r in accesos
                          where r.Accion.ToLower().Contains(opciones.Accion.ToLower())
                          select r;
            }
            if (opciones.Opcion.IsNotNullOrEmptyOrWhiteSpace())
            {
                accesos = from r in accesos
                          where r.Opcion.ToLower().Contains(opciones.Opcion.ToLower())
                          select r;
            }
            if (opciones.Modulo.IsNotNullOrEmptyOrWhiteSpace())
            {
                accesos = from r in accesos
                          where r.Modulo.ToLower().Contains(opciones.Modulo.ToLower())
                          select r;
            }

            if (opciones.Inicio != null || opciones.Fin != null)
            {
                if (opciones.Inicio != null && opciones.Fin == null) //Desde
                {
                    accesos = from r in accesos
                              where r.FechaCaducidad.Value.Date >= opciones.Inicio.Value.Date
                              select r;
                }
                else if (opciones.Inicio == null && opciones.Fin != null) //hasta
                {
                    accesos = from r in accesos
                              where r.FechaCaducidad.Value.Date <= opciones.Fin.Value.Date
                              select r;
                }
                else//entre
                {
                    accesos = from r in accesos
                              where r.FechaCaducidad.Value.Date >= opciones.Inicio.Value.Date && r.FechaCaducidad.Value.Date <= opciones.Fin.Value.Date
                              select r;
                }
            }

            return accesos;
        }

        /// <summary>
        /// Obtiene los permisos que el usuario posee en sus roles de usuario sobre los Accesos Solicitados.
        /// </summary>
        public IEnumerable<Dominio.Seguridad.Entidades.RecursoAccion> ObtenerPermisosDeUsuarioAcciones(List<int> idsAcciones, string subjectId)
        {
            var rolesDeUsuario = from r in UnitOfWork.Set<RolParticularVista>()
                                 where r.SubjectId == subjectId
                                 select r;

            var recursosAcciones = (from p in UnitOfWork.Set<RecursoAccion>()
                                    join r in rolesDeUsuario
                                    on p.IdRol equals r.IdRol
                                    where idsAcciones.Contains(p.IdRecurso)
                                    select p).ToList();

            return recursosAcciones;
        }

    }
}
