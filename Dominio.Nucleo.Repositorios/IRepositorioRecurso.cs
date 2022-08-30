using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Repositorios
{
    /// <summary>
    /// Repositorio que provee las consultas y acciones necesarias para administrar un recurso protegido
    /// </summary>
    /// <typeparam name="TRecurso">Clase que representa un permiso sobre una entidad que es un recurso protegido.</typeparam>
    public interface IRepositorioRecurso<TRecurso>
        where TRecurso : Permiso
    {
        /// <summary>
        /// Obtiene la lista original de recursos de la lista proporcionada.
        /// </summary>
        IEnumerable<TRecurso> ObtenerRecursosOriginales(IEnumerable<IPermiso> recursos);
        /// <summary>
        /// Obtiene los permisos sobre la lista de recursos que el usuario posee
        /// </summary>
        IEnumerable<TRecurso> ObtenerPermisosDeUsuario(IEnumerable<IPermiso> recursos, string idUsuario);
        /// <summary>
        /// Guarda la lista de cambios en una transaccion.
        /// </summary>
        bool GuardarRecursos(IEnumerable<TRecurso> paraAgregar, IEnumerable<TRecurso> paraModificar, IEnumerable<TRecurso> paraEliminar);
        /// <summary>
        /// Obtiene una lista de permisos de un recurso protegido para el usuario indicado.
        /// </summary>
        ConsultaPaginada<IPermisoModel> ConsultarRecursos(IModeloConsultaRecurso parametros, string idUsuario);
    }
}