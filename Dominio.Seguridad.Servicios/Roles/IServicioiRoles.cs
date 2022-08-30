using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Seguridad.Servicios.Roles
{
    public interface IServicioRoles : IServicioRecursoBase<RecursoRol>
    {
        /// <summary>
        /// Valida las reglas para la creacion de un rol
        /// </summary>
        Respuesta<Entidades.Rol> Crear(Entidades.Rol rol, int conMismoNombre, IEnumerable<IRolItem> rolesParticulares, string subjectId);
        /// <summary>
        /// Revisa si un rol puede ser modificado.
        /// </summary>
        Respuesta<Entidades.Rol> Modificar(Entidades.Rol rol, int conMismoNombre, IEnumerable<IPermiso> recursos, string subjectId);
        /// <summary>
        /// Permite revisar si la lista de roles puede ser eliminada.
        /// </summary>
        Respuesta<List<Entidades.Rol>> Eliminar(IEnumerable<Entidades.Rol> roles, string subjectId);

        /// <summary>
        /// Método que valida los permisos sobre los recursos de los roles solicitados
        /// </summary>
        Respuesta ValidarPermisos(IEnumerable<IPermiso> recursos);

        /// <summary>
        /// Método que valida si los roles solicitados se encuentran todos activos
        /// </summary>
        Respuesta ObtenerRolesPorResponsable(List<Rol> roles);

        /// <summary>
        /// Valida si un rol obtenido tiene permisis y está activo.
        /// </summary>
        Respuesta<Rol> ObtenerRol(Rol rol, IEnumerable<IPermiso> recursos);

        /// <summary>
        /// Valida si un rol obtenido está activo.
        /// </summary>
        Respuesta<Rol> ObtenerRol(Rol rol);
    }
}
