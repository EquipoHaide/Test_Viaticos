using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Servicios;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Extensions;

namespace Aplicacion.Nucleo
{
    public abstract class ServicioRecursoBase<TRecurso> : IServicioRecursoBase where TRecurso : Permiso, new()
    {
        const string TAG = "Aplicacion.Nucleo.ServicioRecursoBase";

        public virtual IServicioRecursoBase<TRecurso> ServicioDominio { get; }
        public virtual IRepositorioRecurso<TRecurso> RepositorioRecurso { get; }
        public virtual IServicioRoles ServicioDeRoles { get; }

        /// <summary>
        /// Obtiene una pagina de permisos que el usuario puede administrar filtrada por los parametros indicados.
        /// </summary>
        public abstract Respuesta<ConsultaPaginada<IPermisoModel>> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId);

        /// <summary>
        /// Permite la administracion de permisos de un tipo de recurso.
        /// </summary>
        public Respuesta<List<IPermisoModel>> AdministrarRecursos(IEnumerable<IPermisoModel> permisos, string subjectId)
        {
            if (permisos == null || !permisos.Any()) return new Respuesta<List<IPermisoModel>>("La lista no contiene elementos.", TAG);

            if (subjectId.IsNullOrEmptyOrWhiteSpace()) return new Respuesta<List<IPermisoModel>>("El usuario no existe.", TAG);

            var recursosRol = ServicioDeRoles.RecursosDeRolPorUsuario(permisos.First().IdRol, subjectId);

            var permisosOriginales = RepositorioRecurso.ObtenerRecursosOriginales(permisos);
            var recursosDeUsuario = RepositorioRecurso.ObtenerPermisosDeUsuario(permisos, subjectId);

            Dictionary<IPermisoModel, IPermisoModel> permisosParaValidar = new Dictionary<IPermisoModel, IPermisoModel>();

            foreach (var permiso in permisos)
            {
                var value = permiso.Copy();
                if (permiso.Id > 0)
                {
                    var permisoOriginal = permisosOriginales.Where(p => p.Id == permiso.Id).FirstOrDefault();
                    if (permisoOriginal != null)
                    {
                        value.EsLectura = value.EsLectura != permisoOriginal.EsLectura;
                        value.EsEscritura = value.EsEscritura != permisoOriginal.EsEscritura;
                    }
                }

                permisosParaValidar.Add(permiso, value);
            }

            var respuesta = ServicioDominio.AdministrarRecursos(permisosParaValidar, recursosDeUsuario, recursosRol, subjectId);
            if (!respuesta.EsExito) return new Respuesta<List<IPermisoModel>>(respuesta.Mensaje, respuesta.TAG, respuesta.Estado);

            List<TRecurso> paraAgregar = respuesta.Contenido.Where(r => r.Id <= 0 && (r.EsLectura || r.EsEscritura || r.EsEjecucion)).ToList();
            paraAgregar = paraAgregar?.Count() <= 0 ? null : paraAgregar;

            List<TRecurso> paraModificar = respuesta.Contenido.Where(r => r.Id > 0 && (r.EsLectura || r.EsEscritura || r.EsEjecucion)).ToList();
            paraModificar = paraModificar?.Count() <= 0 ? null : paraModificar;
            if (paraModificar != null)
            {
                foreach (var permiso in paraModificar)
                {
                    var permisoOriginal = permisosOriginales.Where(p => p.Id == permiso.Id).FirstOrDefault();
                    if (permisoOriginal != null)
                    {
                        permiso.IdUsuarioCreo = permisoOriginal.IdUsuarioCreo;
                        permiso.FechaCreacion = permisoOriginal.FechaCreacion;
                    }
                }
            }

            List<TRecurso> paraEliminar = respuesta.Contenido.Where(r => r.Id > 0 && !r.EsLectura && !r.EsEscritura && !r.EsEjecucion).ToList();
            paraEliminar = paraEliminar?.Count() <= 0 ? null : paraEliminar;

            try
            {
                var exito = RepositorioRecurso.GuardarRecursos(paraAgregar, paraModificar, paraEliminar);

                var idsRecurso = paraAgregar != null ? paraAgregar.Select(e => e.IdRecurso).ToList() : new List<int>();
                var nuevos = permisos.Where(p => idsRecurso.Contains(p.IdRecurso)).ToList();
                nuevos.ForEach(e => e.Id = paraAgregar.First(p => p.IdRecurso == e.IdRecurso).Id);

                if (exito)
                {
                    return new Respuesta<List<IPermisoModel>>(nuevos);
                }
                else
                {
                    return new Respuesta<List<IPermisoModel>>("Ocurrio un error, Los datos no pudieron ser guardados.", TAG);
                }
            }
            catch (Exception ex)
            {
                return new Respuesta<List<IPermisoModel>>(ex, TAG) { Mensaje = "Ocurrio un error al intentar conectarse con la base de datos." };
            }

        }
    }
}
