using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
//using MicroServices.Platform.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguridad.Servicios.Grupos
{
    public class ServicioGrupos : ServicioRecursoBase<RecursoGrupo>, IServicioGrupos
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioGrupos";

        /// <summary>
        /// Valida que los roles puedan ser agregados al grupo y retorna una lista con los cambios.
        /// </summary>
        public Respuesta<List<RolGrupo>> AdministrarRoles(List<RolDeGrupoBase> roles, List<RecursoGrupo> permisosGrupo, IEnumerable<IPermiso> permisosRoles, string subjectId)
        {
            if (roles == null || roles.Count <= 0) return new Respuesta<List<RolGrupo>>(new List<RolGrupo>());

            if (roles.Any(r => r.IdRol <= 0)) return new Respuesta<List<RolGrupo>>(R.strings.RolAdministradoInvalido, TAG);

            if (!permisosGrupo.EsAdministrable(false, true)) return new Respuesta<List<RolGrupo>>(R.strings.UsuarioNoTienePermisos, TAG);

            List<RolGrupo> result = new List<RolGrupo>();
            foreach (var rol in roles)
            {
                if (!permisosRoles.Where(p => p.IdRecurso == rol.IdRol).TieneEjecucion())
                {
                    return new Respuesta<List<RolGrupo>>(R.strings.UsuarioNoTienePermisos, TAG);
                }

                if ((rol.Id > 0 && !rol.EsAsignado) || (rol.Id <= 0 && rol.EsAsignado))
                {
                    RolGrupo rolItem = new RolGrupo()
                    {
                        Id = rol.Id,
                        IdRol = rol.IdRol,
                        IdGrupo = rol.IdGrupo
                    };

                    if (rolItem.Id <= 0)
                    {
                        rolItem.Seguir(subjectId);
                    }
                    result.Add(rolItem);
                }
            }

            return new Respuesta<List<RolGrupo>>(result);
        }

        /// <summary>
        /// Valida que los usuarios puedan ser agregados al grupo y retorna una lista con los cambios.
        /// </summary>
        public Respuesta<List<UsuarioGrupo>> AdministrarUsuarios(List<UsuarioDeGrupoBase> usuarios, List<RecursoGrupo> permisos, string subjectId)
        {
            if (usuarios == null || usuarios.Count <= 0) return new Respuesta<List<UsuarioGrupo>>(new List<UsuarioGrupo>());

            if (usuarios.Any(r => r.IdUsuario <= 0)) return new Respuesta<List<UsuarioGrupo>>(R.strings.UsuarioAsignadoNoExiste, TAG);

            if (!permisos.TieneEjecucion()) return new Respuesta<List<UsuarioGrupo>>(R.strings.UsuarioNoTienePermisos, TAG);

            List<UsuarioGrupo> result = new List<UsuarioGrupo>();
            foreach (var usr in usuarios)
            {
                if ((usr.Id > 0 && !usr.EsAsignado) || (usr.Id <= 0 && usr.EsAsignado))
                {
                    UsuarioGrupo usrItem = new UsuarioGrupo()
                    {
                        Id = usr.Id,
                        IdUsuario = usr.IdUsuario,
                        IdGrupo = usr.IdGrupo
                    };

                    if (usrItem.Id <= 0)
                    {
                        usrItem.Seguir(subjectId);
                    }
                    result.Add(usrItem);
                }
            }

            return new Respuesta<List<UsuarioGrupo>>(result);
        }

        /// <summary>
        /// Valida las reglas para la creacion de un grupo
        /// </summary>
        public Respuesta<Entidades.Grupo> Crear(Entidades.Grupo grupo, int conMismoNombre, IEnumerable<IRolItem> rolesParticulares, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Entidades.Grupo>(R.strings.UsuarioInvalido, TAG);

            if (grupo == null)
                return new Respuesta<Entidades.Grupo>(R.strings.GrupoNulo, TAG);

            if (grupo.Nombre.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Entidades.Grupo>(R.strings.GrupoNombreVacio, TAG);

            if (conMismoNombre > 0)
                return new Respuesta<Entidades.Grupo>(String.Format(R.strings.RolNombreDuplicado, grupo.Nombre), TAG);

            grupo.Seguir(subjectId);
            grupo.CrearRecursos(subjectId, rolesParticulares);

            return new Respuesta<Entidades.Grupo>(grupo);
        }

        /// <summary>
        /// Revisa si un grupo puede ser modificado.
        /// </summary>
        public Respuesta<Entidades.Grupo> Modificar(Entidades.Grupo grupo, int conMismoNombre, IEnumerable<RecursoGrupo> recursos, string subjectId)
        {
            if (subjectId.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Entidades.Grupo>(R.strings.UsuarioInvalido, TAG);

            if (grupo == null)
                return new Respuesta<Entidades.Grupo>(R.strings.GrupoNulo, TAG);

            if (!grupo.Activo)
                return new Respuesta<Entidades.Grupo>(R.strings.RegistroInactivo, TAG);

            if (grupo.Nombre.IsNullOrEmptyOrWhiteSpace())
                return new Respuesta<Entidades.Grupo>(R.strings.GrupoNombreVacio, TAG);

            if (conMismoNombre > 0)
                return new Respuesta<Entidades.Grupo>(String.Format(R.strings.RolNombreDuplicado, grupo.Nombre), TAG);

            if (!recursos.EsEditable())
                return new Respuesta<Entidades.Grupo>(R.strings.UsuarioNoTienePermisos, TAG);

            grupo.Seguir(subjectId, true, false);

            return new Respuesta<Entidades.Grupo>(grupo);
        }

        /// <summary>
        /// Permite revisar si la lista de grupos puede ser eliminada.
        /// </summary>
        public Respuesta<List<Entidades.Grupo>> Eliminar(IEnumerable<Entidades.Grupo> grupos)
        {
            if (grupos == null || grupos.Count() <= 0) return new Respuesta<List<Entidades.Grupo>>(R.strings.ListaVacia, TAG);

            List<Entidades.Grupo> result = new List<Entidades.Grupo>();
            bool sonEliminables = true;
            foreach (var grupo in grupos)
            {
                if (!grupo.Recursos.EsEditable())
                {
                    sonEliminables = false;
                    break;
                }
                result.Add(grupo);
            }

            if (!sonEliminables) return new Respuesta<List<Entidades.Grupo>>(R.strings.UsuarioNoTienePermisos, TAG);

            return new Respuesta<List<Entidades.Grupo>>(result);
        }
    }
}
