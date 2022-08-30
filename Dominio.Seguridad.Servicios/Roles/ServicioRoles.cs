using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguridad.Servicios.Roles
{
    public class ServicioRoles : ServicioRecursoBase<RecursoRol>, IServicioRoles
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioRoles";

        /// <summary>
        /// Valida las reglas para la creacion de un rol
        /// </summary>
        public Respuesta<Rol> Crear(Rol rol, int conMismoNombre, IEnumerable<IRolItem> rolesParticulares, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Rol>(R.strings.UsuarioInvalido, TAG);

            if (rol == null)
                return new Respuesta<Rol>(R.strings.RolNulo, TAG);

            if (rol.Nombre.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Rol>(R.strings.RolNombreVacio, TAG);

            if (conMismoNombre > 0)
                return new Respuesta<Rol>(String.Format(R.strings.RolNombreDuplicado, rol.Nombre), TAG);

            rol.Seguir(subjectId);
            rol.CrearRecursos(subjectId, rolesParticulares);

            return new Respuesta<Rol>(rol);
        }

        /// <summary>
        /// Revisa si un rol puede ser modificado.
        /// </summary>
        public Respuesta<Rol> Modificar(Rol rol, int conMismoNombre, IEnumerable<IPermiso> recursos, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Rol>(R.strings.UsuarioInvalido, TAG);

            if (rol == null)
                return new Respuesta<Rol>(R.strings.RolNulo, TAG);

            if (!rol.Activo)
                return new Respuesta<Rol>(R.strings.RegistroInactivo, TAG);

            if (rol.Nombre.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Rol>(R.strings.RolNombreVacio, TAG);

            if (conMismoNombre > 0)
                return new Respuesta<Rol>(String.Format(R.strings.RolNombreDuplicado, rol.Nombre), TAG);

            if (!recursos.EsEditable())
                return new Respuesta<Rol>(R.strings.UsuarioNoTienePermisos, TAG);

            rol.Seguir(subjectId, true, false);
            return new Respuesta<Rol>(rol);
        }

        /// <summary>
        /// Permite revisar si la lista de roles puede ser eliminada.
        /// </summary>
        public Respuesta<List<Rol>> Eliminar(IEnumerable<Rol> roles, string subjectId)
        {
            if (roles == null || !roles.Any())
                return new Respuesta<List<Rol>>(R.strings.ListaVacia, TAG);

            List<Rol> result = new List<Rol>();
            bool sonEliminables = true;

            foreach (var rol in roles)
            {
                if (!rol.Recursos.EsEditable())
                {
                    sonEliminables = false;
                    break;
                }

                rol.Seguir(subjectId, true);
                result.Add(rol);
            }

            if (!sonEliminables)
                return new Respuesta<List<Rol>>(R.strings.UsuarioNoTienePermisos, TAG);

            return new Respuesta<List<Rol>>(result);
        }

        /// <summary>
        /// Método que valida los permisos sobre los recursos de los roles solicitados
        /// </summary>
        public Respuesta ValidarPermisos(IEnumerable<IPermiso> recursos)
        {
            if (recursos is null || !recursos.Any())
                return new Respuesta(R.strings.ListaVacia, TAG);

            if (!recursos.TieneLectura() && !recursos.TieneEjecucion())
                return new Respuesta(R.strings.UsuarioNoTienePermisos, TAG);

            return new Respuesta();
        }

        /// <summary>
        /// Método que valida si los roles solicitados se encuentran todos activos
        /// </summary>
        public Respuesta ObtenerRolesPorResponsable(List<Rol> roles)
        {
            if (roles is null || !roles.Any())
                return new Respuesta(R.strings.ListaVacia, TAG);

            return new Respuesta();
        }

        /// <summary>
        /// Valida si un rol obtenido tiene permisis y está activo.
        /// </summary>
        public Respuesta<Rol> ObtenerRol(Rol rol, IEnumerable<IPermiso> recursos)
        {
            if (rol == null)
                return new Respuesta<Rol>(R.strings.RolNulo, TAG);

            if (!rol.Activo)
                return new Respuesta<Rol>(String.Format(R.strings.RecursoRolInactivo, rol.Nombre), TAG);

            var respuesta = this.ValidarPermisos(recursos);

            if (respuesta.EsError)
                return new Respuesta<Rol>(respuesta.Mensaje, respuesta.TAG);

            return new Respuesta<Rol>(rol);
        }

        /// <summary>
        /// Valida si un rol obtenido está activo.
        /// </summary>
        public Respuesta<Rol> ObtenerRol(Rol rol)
        {
            if (rol == null)
                return new Respuesta<Rol>(R.strings.RolNulo, TAG);

            if (!rol.Activo)
                return new Respuesta<Rol>(String.Format(R.strings.RecursoRolInactivo, rol.Nombre), TAG);

            return new Respuesta<Rol>(rol);
        }
    }
}
