using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    /// <summary>
    /// Representa un servicio de negocio que establece las reglas sobre el manejo de recursos protegidos.
    /// </summary>
    /// <typeparam name="TRecurso">Clase que representa un permiso sobre una entidad que es un recurso protegido.</typeparam>
    public class ServicioRecursoBase<TRecurso> : IServicioRecursoBase<TRecurso>
        where TRecurso : Permiso, new()
    {
        public const string TAG = "Dominio.Nucleo.Servicios.ServicioRecursoBase";

        /// <summary>
        /// Valida todas las reglas necesarias que se aplicaran a la administración de los recursos protegidos de una entidad.
        /// </summary>
        /// <param name="permisos">diccionario de permisos que representan los cambios realizados por el usuario y el permiso original.</param>
        /// <param name="recursos">Permisos que el usuario posee sobre los permisos que se estan administrando.</param>
        /// <param name="recursosRol">Permisis que el usuario posee sobre el rol que se esta administrando.</param>
        /// <param name="idUsuario">Usuario que esta realizando la administracion de recursos.</param>
        public Respuesta<List<TRecurso>> AdministrarRecursos(Dictionary<IPermisoModel, IPermisoModel> permisos, IEnumerable<IPermiso> recursos, IEnumerable<IPermiso> recursosRol, string idUsuario)
        {
            if (permisos == null || permisos?.Count <= 0)
                return new Respuesta<List<TRecurso>>("La lista no contiene elementos.", TAG);

            if (!recursosRol.TieneEjecucion())
                return new Respuesta<List<TRecurso>>("El usuario no cuenta con los permisos suficientes para realizar la acción correspondiente.", TAG);

            bool sonAdministrables = true;
            List<TRecurso> result = new List<TRecurso>();

            foreach (var permiso in permisos)
            {
                if (!recursos.EsAdministrable(permiso.Value.EsLectura, permiso.Value.EsEscritura, permiso.Value.IdRecurso))
                {
                    sonAdministrables = false;
                    break;
                }

                var newRecurso = permiso.Key.ToEntity<TRecurso>();
                newRecurso.Seguir(idUsuario, newRecurso.Id > 0);
                result.Add(newRecurso);
            }

            if (!sonAdministrables)
                return new Respuesta<List<TRecurso>>("El usuario no cuenta con los permisos suficientes para realizar la acción correspondiente.", TAG);

            return new Respuesta<List<TRecurso>>(result);
        }
    }
}
