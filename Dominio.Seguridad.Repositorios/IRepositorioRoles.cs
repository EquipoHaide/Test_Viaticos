using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;
using System.Collections.Generic;


namespace Dominio.Seguridad.Repositorios
{
    public interface IRepositorioRoles : IRepository<Entidades.Rol>, IRepositorioRecurso<RecursoRol>
    {
        /// <summary>
        /// Obtiene los recursos del rol indicado para el usuario establecido.
        /// </summary>
        IEnumerable<RecursoRol> RecursosDeRolPorUsuario(int idRol, string subjectId);
        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido.
        /// </summary>
        IEnumerable<RecursoRol> RecursosDeRolPorUsuario(IEnumerable<IRolItem> roles, string subjectId);
        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido en sus roles particulares.
        /// </summary>
        IEnumerable<RecursoRol> RecursosDeRolPorUsuarioParticulares(IEnumerable<IRolItem> roles, string subjectId);
        /// <summary>
        /// Obtiene los roles particulares del usuario indicado.
        /// </summary>
        IEnumerable<RolParticularVista> ObtenerRolesParticulares(string subjectId);
        /// <summary>
        /// Obtiene los roles directos del usuario indicado.
        /// </summary>
        IEnumerable<RolDirectoVista> ObtenerRolesDirectos(string subjectId);
        /// <summary>
        /// Obtiene los roles de usuario del usuario indicado.
        /// </summary>
        IEnumerable<RolUsuarioVista> ObtenerRolesUsuarios(string subjectId);
        /// <summary>
        /// Obtiene un contador con la cantidad de roles que tienen el mismo nombre exceptuando el rol indicado 
        /// </summary>
        int ObtenerConMismoNombre(string nombre, int idRol = 0);
        /// <summary>
        /// Obtiene una lista de roles cargados con los permisos que el usuario posee sobre ellos.
        /// </summary>
        IEnumerable<Entidades.Rol> ObtenerConRecursos(IEnumerable<Modelos.Rol> roles, string subjectId);
        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Modelos.Rol ObtenerRol(int id, string subjectId);
        /// <summary>
        /// Consulta paginada de Roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        ConsultaPaginada<Modelos.Rol> ObtenerRoles(ConsultaRol filtro, string subjectId);
        /// <summary>
        /// Obtiene la lista de roles que el usuario puede leer con sus roles particulares. 
        /// </summary>
        ConsultaPaginada<Modelos.Rol> ObtenerRolesPorRolesParticulares(ConsultaRol filtro, string subjectId);

        /// <summary>
        /// Remueve roles de grupo a los roles.
        /// </summary>
        void RemoverRolGrupos(List<int> ids);

        /// <summary>
        /// Remueve roles de usuario a los roles.
        /// </summary>
        void RemoverRolUsuarios(List<int> ids);

        /// <summary>
        /// Obtiene una lista de ID y NombreRol a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        List<(int idRol, string Rol)> ObtenerRoles(List<int> idRoles, string subjectId, bool requerieFiltroPorPermisos = false);

        /// <summary>
        /// Obtiene una lista de roles a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        List<Modelos.Rol> ObtenerRoles(string nombre, string subjectId);

        /// <summary>
        /// Obtiene los recursos de los roles indicados para el usuario establecido.
        /// </summary>
        IEnumerable<RecursoRol> ObtenerPermisosDeUsuariorRol(List<int> idsRol, string subjectId);

        /// <summary>
        /// Obtiene los roles indicados
        /// </summary>
        List<Entidades.Rol> ObtenerRoles(List<int> idsRol);

        /// <summary>
        /// Consulta paginada de Roles a los que tiene permiso de lectura el usuario.
        /// </summary>
        ConsultaPaginada<Modelos.Rol> ConsultarRoles(ConsultaRol filtro, string subjectId);

        /// <summary>
        /// Obtiene un rol por su id y si tiene permiso de lectura usuario.
        /// </summary>
        public Modelos.Rol ObtenerRolPorLectura(int id, string subjectId);
    }
}
