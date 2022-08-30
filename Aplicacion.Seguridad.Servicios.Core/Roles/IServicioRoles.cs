using Aplicacion.Nucleo;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;


namespace Aplicacion.Seguridad.Servicios.Core
{
    public interface IServicioRoles : Nucleo.IServicioRoles, IServicioRecursoBase
    {
        /// <summary>
        /// Agrega un nuevo rol.
        /// </summary>
        Respuesta<Rol> Crear(Rol rol, string subjectId);

        /// <summary>
        /// Permite la modificacion de un rol
        /// </summary>
        Respuesta Modificar(Rol rol, string subjectId);

        /// <summary>
        /// Permite eliminar una lista de roles.
        /// </summary>
        Respuesta Eliminar(List<Rol> roles, string subjectId);

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<Rol> Obtener(int id, string subjectId);

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<Dominio.Seguridad.Entidades.Rol> ObtenerRol(int id, string subjectId);

        /// <summary>
        /// Obtiene un rol por su id
        /// </summary>
        Respuesta<Dominio.Seguridad.Entidades.Rol> ObtenerRol(int id, bool requiereValidacion = true);

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<ConsultaPaginada<Rol>> Consultar(ConsultaRol filtro, string subjectId);

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura y ejecucion el usuario a travez de sus roles particulares.
        /// </summary>
        Respuesta<ConsultaPaginada<Rol>> ConsultarPorRolesParticulares(ConsultaRol filtro, string subjectId);

        /// <summary>
        /// Obtiene una lista de ID y NombreRol a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<List<(int idRol, string Rol)>> ObtenerRoles(List<int> idRoles, string subjectId, bool requerieFiltroPorPermisos = false);

        /// <summary>
        /// Obtiene una lista de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<List<Rol>> ObtenerRoles(string nombre, string subjectId);

        /// <summary>
        /// Método que valida permisos de lectura sobre el recurso rol
        /// </summary>
        Respuesta ValidarPermisosRecursoRol(List<int> ids, string subjectId);

        /// <summary>
        /// Método que valida si los roles solicitados se encuentran activos
        /// </summary>
        Respuesta<List<Dominio.Seguridad.Entidades.Rol>> ObtenerRolesPorResponsable(List<int> ids);

        /// <summary>
        /// Consulta paginada de roles a los que tiene permiso de lectura el usuario.
        /// </summary>
        Respuesta<ConsultaPaginada<Rol>> ConsultarRoles(ConsultaRol filtro, string subjectId);

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura el usuario.
        /// </summary>
        Respuesta<Rol> ObtenerPorLectura(int id, string subjectId);
    }
}
