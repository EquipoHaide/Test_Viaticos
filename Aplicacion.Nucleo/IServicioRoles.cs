using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo
{
    public interface IServicioRoles
    {
        /// <summary>
        /// Obtiene los permisos sobre el rol indicado que el usuario indicado posee
        /// </summary>
        IEnumerable<IPermiso> RecursosDeRolPorUsuario(int idRol, string subjectId);
        /// <summary>
        /// Obtiene los permisos sobre los roles indicados que el usuario indicado posee
        /// </summary>
        IEnumerable<IPermiso> RecursosDeRolPorUsuario(IEnumerable<IRolItem> roles, string subjectId);
        /// <summary>
        /// Obtiene los permisos sobre los roles indicados que el usuario indicado posee con sus roles particulares
        /// </summary>
        IEnumerable<IPermiso> RecursosDeRolPorUsuarioParticulares(IEnumerable<IRolItem> roles, string subjectId);
        /// <summary>
        /// Obtiene la lista de roles particulares del usuario indicado.
        /// </summary>
        Respuesta<IEnumerable<RolParticularVista>> ObtenerRolesParticulares(string subjectId);
        /// <summary>
        /// Obtiene la lista de roles directos del usuario indicado.
        /// </summary>
        Respuesta<IEnumerable<RolDirectoVista>> ObtenerRolesDirectos(string subjectId);
        /// <summary>
        /// Obtiene la lista de roles del usuario indicado.
        /// </summary>
        Respuesta<IEnumerable<RolUsuarioVista>> ObtenerRolesUsuarios(string subjectId);
    }
}