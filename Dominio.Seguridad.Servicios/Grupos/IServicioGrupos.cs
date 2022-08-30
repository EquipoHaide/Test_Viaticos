using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;


namespace Dominio.Seguridad.Servicios.Grupos
{
    public interface IServicioGrupos : IServicioRecursoBase<RecursoGrupo>
    {
        /// <summary>
        /// Valida que los usuarios puedan ser agregados al grupo y retorna una lista con los cambios.
        /// </summary>
        Respuesta<List<UsuarioGrupo>> AdministrarUsuarios(List<UsuarioDeGrupoBase> usuarios, List<RecursoGrupo> permisos, string subjectId);
        /// <summary>
        /// Valida que los roles puedan ser agregados al grupo y retorna una lista con los cambios.
        /// </summary>
        Respuesta<List<RolGrupo>> AdministrarRoles(List<RolDeGrupoBase> roles, List<RecursoGrupo> permisosGrupo, IEnumerable<IPermiso> permisosRoles, string subjectId);
        /// <summary>
        /// Valida las reglas para la creacion de un grupo
        /// </summary>
        Respuesta<Entidades.Grupo> Crear(Entidades.Grupo grupo, int conMismoNombre, IEnumerable<IRolItem> rolesParticulares, string subjectId);
        /// <summary>
        /// Revisa si un grupo puede ser modificado.
        /// </summary>
        Respuesta<Entidades.Grupo> Modificar(Entidades.Grupo grupo, int conMismoNombre, IEnumerable<RecursoGrupo> recursos, string subjectId);
        /// <summary>
        /// Permite revisar si la lista de grupos puede ser eliminada.
        /// </summary>
        Respuesta<List<Entidades.Grupo>> Eliminar(IEnumerable<Entidades.Grupo> grupos);
    }
}
