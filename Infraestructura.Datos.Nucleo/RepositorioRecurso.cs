using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Datos.Nucleo
{
    public abstract class RepositorioRecurso<TEntidad, TRecurso> : Repository<TEntidad>, IRepositorioRecurso<TRecurso>
        where TEntidad : class
        where TRecurso : Permiso
    {
        public RepositorioRecurso(IUnitOfWork unitOfWork) : base(unitOfWork) { }


        /// <summary>
        /// Obtiene una lista de permisos de un recurso protegido para el usuario indicado.
        /// </summary>
        public abstract ConsultaPaginada<IPermisoModel> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId);
        /// <summary>
        /// Guarda la lista de cambios en una transaccion.
        /// </summary>
        public bool GuardarRecursos(IEnumerable<TRecurso> paraAgregar, IEnumerable<TRecurso> paraModificar, IEnumerable<TRecurso> paraEliminar)
        {
            var recursosContext = this.UnitOfWork.Set<TRecurso>();

            if (paraAgregar != null && paraAgregar.Count() > 0)
                recursosContext.AddRange(paraAgregar);

            if (paraModificar != null && paraModificar.Count() > 0)
                recursosContext.UpdateRange(paraModificar);

            if (paraEliminar != null && paraEliminar.Count() > 0)
                recursosContext.RemoveRange(paraEliminar);

            using (var transaccion = new TransactionScope())
            {
                try
                {
                    var result = this.Save();
                    transaccion.Complete();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    transaccion.Dispose();
                    throw ex;
                }
            }
        }
        /// <summary>
        /// Obtiene los permisos sobre la lista de recursos que el usuario posee
        /// </summary>
        public IEnumerable<TRecurso> ObtenerPermisosDeUsuario(IEnumerable<IPermiso> recursos, string idUsuario)
        {
            var Permisos = UnitOfWork.Set<TRecurso>();
            var rolesParticulares = UnitOfWork.Set<RolParticularVista>();

            var recursosId = recursos.Select(r => r.IdRecurso);


            var permisos = from p in Permisos
                           join r in UnitOfWork.Set<RolParticularVista>()
                                on p.IdRol equals r.IdRol
                           where r.SubjectId == idUsuario
                           && recursosId.Contains(p.IdRecurso)
                           select p;

            return permisos.WithTracking(false).ToList();
        }
        /// <summary>
        /// Obtiene la lista original de recursos de la lista proporcionada.
        /// </summary>
        public IEnumerable<TRecurso> ObtenerRecursosOriginales(IEnumerable<IPermiso> recursos)
        {
            var recursosId = recursos.Select(r => r.Id);

            var result = from r in UnitOfWork.Set<TRecurso>()
                         where recursosId.Contains(r.Id)
                         select r;

            return result.WithTracking(false).ToList();
        }

    }
}
