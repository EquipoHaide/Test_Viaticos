using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    /// <summary>
    /// Representa un servicio de negocio que establece las reglas sobre el manejo de recursos protegidos.
    /// </summary>
    /// <typeparam name="TRecurso">Clase que representa un permiso sobre una entidad que es un recurso protegido.</typeparam>
    public interface IServicioRecursoBase<TRecurso>
        where TRecurso : Permiso, new()
    {
        /// <summary>
        /// Valida todas las reglas necesarias que se aplicaran a la administración de los recursos protegidos de una entidad.
        /// </summary>
        /// <param name="permisos">diccionario de permisos que representan los cambios realizados por el usuario y el permiso original.</param>
        /// <param name="recursos">Permisos que el usuario posee sobre los permisos que se estan administrando.</param>
        /// <param name="recursosRol">Permisis que el usuario posee sobre el rol que se esta administrando.</param>
        /// <param name="idUsuario">Usuario que esta realizando la administracion de recursos.</param>
        Respuesta<List<TRecurso>> AdministrarRecursos(Dictionary<IPermisoModel, IPermisoModel> permisos, IEnumerable<IPermiso> recursos, IEnumerable<IPermiso> recursosRol, string idUsuario);
    }
}