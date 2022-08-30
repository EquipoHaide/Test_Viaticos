using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Modelos = Dominio.Seguridad.Modelos;

namespace Aplicacion.Seguridad.Servicios
{
    public interface IServicioUsuarios
    {

        /// <summary>
        /// Retorna el estado de un usuario
        /// </summary>
        Respuesta<bool> EsUsuarioHabilitado(string subjectId);
        /// <summary>
        /// Permite iniciar sesion si el usuario lo tiene permitido.
        /// </summary>
        Task<Respuesta<int>> IniciarSesion(Modelos.Usuario usuario, string token, DateTime expirationToken, string subjectId);
        /// <summary>
        /// Permite actualizar el token de una sesión.
        /// </summary>
        Respuesta ActualizarTokenSesion(int idSesion, string token, DateTime expirationToken);
        /// <summary>
        /// Permite cerrar una sesión de usuario.
        /// </summary>
        Respuesta CerrarSesion(int idSesion);
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        Respuesta<List<Sesion>> ObtenerSesionesActivas(string subjectId);
        /// <summary>
        /// Obtiene una lista de las sesiones activas del usuario.
        /// </summary>
        Respuesta<List<Sesion>> ObtenerSesionesActivas(int idUsuario);
        /// <summary>
        /// Obtiene todos los usuarios que coincidan con los prametros proporcionados.
        /// </summary>
        Respuesta<ConsultaPaginada<UsuarioItem>> ConsultarUsuarios(ConsultarUsuariosModelo parametros);
        /// <summary>
        /// Permite la actualización del estado de un usuario.
        /// </summary>
        Respuesta ActualizarEstado(bool esHabilitado, int idUsuario);
        /// <summary>
        /// Permite la actualización de la cantidad de sesiones que un usuario tiene permitidas simultaneamente.
        /// </summary>
        Respuesta ActualizarSesionesPermitidas(int sesionesPermitidas, int idUsuario);
        /// <summary>
        /// Obtiene el perfil de un usuario registrado.
        /// </summary>
        Respuesta<Modelos.Usuario> ObtenerPerfil(int idUsuario);
        /// <summary>
        /// Obtiene el perfil de un usuario registrado.
        /// </summary>
        Respuesta<Modelos.Usuario> ObtenerPerfil(string subjectId);

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos
        /// </summary>
        Respuesta<Dictionary<string, string>> ObtenerNombreUsuarios(List<string> subjectIds);

        /// <summary>
        /// Obtiene una lista de nombres de usuarios según los usuarios requeridos por ID
        /// </summary>
        Respuesta<Dictionary<int, string>> ObtenerNombreUsuarios(List<int> ids);

        /// <summary>
        /// Obtiene un usuario según el subjectId dado.
        /// </summary>
        Respuesta<Dominio.Seguridad.Entidades.Usuario> ObtenerUsuario(string subjectId);

        /// <summary>
        /// Obtiene un usuario según el subjectId dado.
        /// </summary>
        Respuesta<Dominio.Seguridad.Entidades.Usuario> ObtenerUsuario(int idUsuario);

        /// <summary>
        /// Administra el agregado o eliminado de roles a un usuario.
        /// </summary>
        Respuesta<List<RolDeUsuarioBase>> AdministrarRoles(List<RolDeUsuarioBase> roles, string subjectId);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los grupos que seran eliminados.(Eliminar uno o mas grupos)
        /// </summary>
        Respuesta CompilarRolesPorGrupos(IEnumerable<int> removerGrupos);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles que seran eliminados.(Eliminar uno o mas roles)
        /// </summary>
        Respuesta CompilarRolesPorRoles(IEnumerable<int> removerRoles);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los usuarios asignados o removidos del grupo indicado.(Administrar los usuarios de un grupo)
        /// </summary>
        Respuesta CompilarRolesPorAsignacionUsuarios(IEnumerable<int> nuevosUsuarios, IEnumerable<int> removerUsuarios, int idGrupo);
        /// <summary>
        /// Actualiza las listas de roles de usuario, particulares y directos respecto a los roles asignados o removidos del grupo indicado.(Agregar o quitar roles de un grupo)
        /// </summary>
        Respuesta CompilarRolesPorAsignacionRoles(IEnumerable<int> nuevosRoles, IEnumerable<int> removerRoles, int idGrupo);
        /// <summary>
        /// Ejecuta el guardado de los cambios en el repositorio de usuarios, permitiendo capturar las excepciones en caso de su existencia.
        /// </summary>
        Respuesta GuardarCompilacionRoles();

        /// <summary>
        /// Obtiene los roles que el usuario puede administrar filtrados por los parametros indicados.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeUsuarioBase>> ConsultarRoles(ConsultaRol parametros, string subjectId);

        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        Respuesta<List<UsuarioItem>> ObtenerUsuarios();


        /// <summary>
        /// Método que obtiene una lista de usuarios habilitados
        /// </summary>
        Respuesta<List<UsuarioItem>> ObtenerUsuariosAutocomplete(ConsultarUsuariosModelo consulta);

        /// <summary>
        /// Metodo que obtiene los usuarios que cumplen con algun rol de una lista de roles
        /// </summary>
        /// <param name="idsRoles"></param>
        /// <returns></returns>
        public Respuesta<List<UsuarioNotificacion>> ObtenerUsuariosPorRoles(List<int> idsRoles);

    }
}
