using Dominio.Nucleo.Entidades;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;
using System.Collections.Generic;


namespace Dominio.Seguridad.Repositorios
{
    public interface IRepositorioUsuarios : IRepository<Entidades.Usuario>
    {
        /// <summary>
        /// Obtiene todos los usuarios que coincidan con los prametros proporcionados.
        /// </summary>
        ConsultaPaginada<UsuarioItem> ConsultarUsuarios(ConsultarUsuariosModelo parametros);
        /// <summary>
        /// Agrega una sesión de usuario.
        /// </summary>
        void AgregarSesion(Sesion sesion);
        /// <summary>
        /// Obtiene el usuario con las sesiones activas.
        /// </summary>
        Entidades.Usuario ObtenerUsuarioConSesiones(string subjectId);
        /// <summary>
        /// Obtiene la sesion indicada
        /// </summary>
        Sesion ObtenerSesion(int idSesion);
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        List<Sesion> ObtenerSesionesActivas(string subjectId);
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        List<Sesion> ObtenerSesionesActivas(int idUsuario);
        /// <summary>
        /// Actualiza una sesión.
        /// </summary>
        void ActualizarSesion(Sesion sesion);
        /// <summary>
        /// Permite agregar y remover los roles para el usuario.
        /// </summary>
        void AdministrarRoles(IEnumerable<RolUsuario> paraAgregar, IEnumerable<RolUsuario> paraEliminar);
        /// <summary>
        /// Obtiene un contador de los roles directos del usuario indicado.
        /// Nota el IdUsuario es el subjectId.
        /// </summary>
        UsuarioRolesDirectosContador ContarRolesDirectos(int idUsuario);
        /// <summary>
        /// Agrega roles a la vista de roles directos.
        /// </summary>
        void AgregarRolesDirectos(IEnumerable<RolDirectoVista> rolUsuarioVistas);
        /// <summary>
        /// Agrega roles a la vista de roles de usuario.
        /// </summary>
        void AgregarRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas);
        /// <summary>
        /// Iguala los roles particulares con los roles de usuario omitiendo los roles eliminado.
        /// </summary>
        void IgualarRolesParticulares(IEnumerable<int> rolesEliminados, string subjectId);
        /// <summary>
        /// Remueve roles a la vista de roles directos.
        /// </summary>
        void RemoverRolesDirectos(IEnumerable<RolDirectoVista> rolDirectosVistas);
        /// <summary>
        /// Remueve roles a la vista de roles de usuario.
        /// </summary>
        void RemoverRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas);
        /// <summary>
        /// Remueve roles a la vista de roles particulares.
        /// </summary>
        void RemoverRolesParticulares(string subjectId);
        /// <summary>
        /// Obtiene una lista de roles de usuario el usuario indicado
        /// </summary>
        List<RolVistaBase> ObtenerRolesUsuario(string subjectId);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los grupos que seran eliminados.
        /// </summary>
        Respuesta CompilarRolesPorGrupos(IEnumerable<int> removerGrupos);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que seran eliminados.
        /// </summary>
        Respuesta CompilarRolesPorRoles(IEnumerable<int> removerRoles);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que asignados o removidos de la lista de roles directos del usuario.
        /// </summary>
        Respuesta CompilarRolesPorUsuario(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idUsuario);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los usuarios asignados o removidos del grupo indicado.
        /// </summary>
        Respuesta CompilarRolesPorAsignacionUsuarios(IEnumerable<int> nuevosUsuarios, IEnumerable<int> removerUsuarios, int idGrupo);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles asignados o removidos del grupo indicado.
        /// </summary>
        Respuesta CompilarRolesPorAsignacionRoles(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idGrupo);

        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase> ConsultarRoles(ConsultaRol parametros, string subjectId);

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos
        /// </summary>
        Dictionary<string, string> ObtenerNombreUsuarios(List<string> subjectIds);

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos por ID
        /// </summary>
        Dictionary<int, string> ObtenerNombreUsuarios(List<int> ids);

        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        List<UsuarioItem> ObtenerUsuarios();

        /// <summary>
        /// Retorna el listado de usuarios con funcionalidad de busqueda por criterio
        /// Utilizado en autocomplete de vistas
        /// </summary>
        List<UsuarioItem> ObtenerUsuariosAutocomplete(ConsultarUsuariosModelo consulta);

        /// <summary>
        /// Metodo que obtiene los usuarios que tienen algun rol de una lista de roles
        /// </summary>
        /// <param name="idsRoles"></param>
        /// <returns></returns>
        List<UsuarioNotificacion> ObtenerUsuariosporRoles(List<int> idsRoles);
    }
}
