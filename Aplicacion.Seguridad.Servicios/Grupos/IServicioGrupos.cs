
using Aplicacion.Nucleo;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;

namespace Aplicacion.Seguridad.Servicios
{
    public interface IServicioGrupos : IServicioRecursoBase
    {

        Respuesta<List<RolDeGrupoBase>> AdministrarRoles(List<RolDeGrupoBase> roles, string subjectId);
        Respuesta<List<UsuarioDeGrupoBase>> AdministrarUsuarios(List<UsuarioDeGrupoBase> usuarios, string subjectId);
        Respuesta<Grupo> Crear(Grupo grupo, string subjectId);
        Respuesta Eliminar(List<Grupo> grupos, string subjectId);
        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<Grupo> Obtener(int id, string subjectId);
        Respuesta Modificar(Grupo grupo, string subjectId);
        /// <summary>
        /// Consulta paginada de Grupos a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Respuesta<ConsultaPaginada<Grupo>> Consultar(ConsultaGrupo filtro, string subjectId);

        /// <summary>
        /// Obtienes los roles de un grupo
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Respuesta<ConsultaPaginada<RolDeGrupoItem>> ConsultarRoles(ConsultaRolGrupo parametros, string subjectId);

        /// <summary>
        /// Obtienes los usuarios administrables de un grupo
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        Respuesta<ConsultaPaginada<UsuarioDeGrupoItem>> ConsultarUsuarios(ConsultaUsuariosGrupo parametros, string subjectId);

        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura el usuario.
        /// </summary>
        Respuesta<Grupo> ObtenerPorLectura(int id, string subjectId);

    }
}
