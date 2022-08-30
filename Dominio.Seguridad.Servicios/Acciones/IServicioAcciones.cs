using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;


namespace Dominio.Seguridad.Servicios.Acciones
{
    public interface IServicioAcciones : IServicioRecursoBase<RecursoAccion>
    {
        /// <summary>
        /// Valida que exista un acceso para la ruta.
        /// </summary>
        Respuesta<bool> ValidarAcceso(Acceso acceso);

        Respuesta<List<Modelos.AccesoAsignado>> ValidarAccesos(List<string> acciones, List<Acceso> accesos);

        /// <summary>
        /// Validad que se posea permisos sobre 
        /// </summary>
        /// <returns></returns>
        Respuesta ValidarPermisoAcciones(List<int> idsAccesos, IEnumerable<RecursoAccion> recursos);

        /// <summary>
        /// Crea un acceso
        /// </summary>
        /// <returns></returns>
        Respuesta<Acceso> CrearAcceso(Acceso acceso, string subjectId);

        /// <summary>
        /// Modificar un acceso
        /// </summary>
        /// <returns></returns>
        Respuesta<Acceso> ModificarAcceso(Acceso acceso, string subjectId);

        /// <summary>
        /// Eliminar un acceso
        /// </summary>
        /// <returns></returns>
        Respuesta<Acceso> EliminarAcceso(Acceso acceso, string subjectId);
    }
}
