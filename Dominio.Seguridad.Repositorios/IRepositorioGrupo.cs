using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;
using System.Collections.Generic;


namespace Dominio.Seguridad.Repositorios
{
    public interface IRepositorioGrupos : IRepository<Entidades.Grupo>, IRepositorioRecurso<RecursoGrupo>
    {
        /// <summary>
        /// Obtiene la lista de permisos del usuario en sus roles particulares.
        /// </summary>
        List<RecursoGrupo> ObtenerPermisos(int idGrupo, string subjectId);
        /// <summary>
        /// Agrega y elimina las listas de usuarios del grupo.
        /// </summary>
        void AdministrarUsuarios(IEnumerable<UsuarioGrupo> paraAgregar, IEnumerable<UsuarioGrupo> paraEliminar);
        /// <summary>
        /// Obtiene la lista de los roles que el grupo posee
        /// </summary>
        List<int> ObtenerRolesDelGrupo(int idGrupo);
        /// <summary>
        /// Cuenta cuantos roles directos tiene cada uno de los usuarios de la lista.
        /// Nota: El idUsuario que regresa el es subjectId
        /// </summary>
        IEnumerable<UsuarioRolesDirectosContador> ContarRolesDirectos(IEnumerable<UsuarioRolesDirectosContador> usuario);
        /// <summary>
        /// Cuenta cuantos roles directos tiene cada uno de los usuarios del grupo.
        /// Nota: El idUsuario que regresa el es subjectId
        /// </summary>
        IEnumerable<UsuarioRolesDirectosContador> ContarRolesDirectos(int idGrupo);
        /// <summary>
        /// Agrega roles a la vista de roles de usuario.
        /// </summary>
        void AgregarRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas);
        /// <summary>
        /// Agrega roles a la vista de roles particulares.
        /// </summary>
        void AgregarRolesParticulares(IEnumerable<RolParticularVista> rolUsuarioVistas);
        /// <summary>
        /// Remueve roles a la vista de roles de usuario.
        /// </summary>
        void RemoverRolesDeUsuario(IEnumerable<RolUsuarioVista> rolUsuarioVistas);
        /// <summary>
        /// Remueve roles a la vista de roles particulares.
        /// </summary>
        void RemoverRolesParticulares(IEnumerable<RolParticularVista> rolUsuarioVistas);
        /// <summary>
        /// Agrega y elimina los roles del grupo.
        /// </summary>
        void AdministrarRoles(IEnumerable<RolGrupo> paraAgregar, IEnumerable<RolGrupo> paraEliminar);
        /// <summary>
        /// Obtiene la cantidad de grupos que tengan el mismo nombre. En el caso de recibir el Id del grupo omite ese grupo.
        /// Nota: hkjfksfoj kjhkhsdf hfkjshdfjkjh sfñls  djhgdslkfjghsldfkjhg kdjhlfgskldjfgh
        /// </summary>
        int ObtenerGruposConElMismoNombre(string nombre, int idGrupo = 0);
        /// <summary>
        /// Obtiene los permisos que el usuario posee en sus roles de usuario sobre el grupo.
        /// </summary>
        IEnumerable<RecursoGrupo> ObtenerPermisosDeUsuario(int idGrupo, string subjectId);
        /// <summary>
        /// Obtiene los grupos indicados cargados con los recursos que el usuario posee con sus roles de usuario.
        /// </summary>  
        IEnumerable<Entidades.Grupo> ObtenerConRecursos(IEnumerable<Modelos.Grupo> grupos, string subjectId);

        /// <summary>
        /// Consulta paginada de Grupos a los que tiene permiso de lectura y escritura el usuario.
        /// </summary>
        ConsultaPaginada<Modelos.Grupo> ObtenerGrupos(ConsultaGrupo filtro, string subjectId);

        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura y escritura el usuario.
        /// </summary>
        Modelos.Grupo ObtenerGrupo(int id, string subjectId);


        /// <summary>
        /// Obtiene los recursos del grupo filtrado por los parametros indicados.
        /// </summary>
        ConsultaPaginada<Dominio.Seguridad.Modelos.RolDeGrupoItem> ConsultarRoles(ConsultaRolGrupo parametros, string subjectId);

        /// <summary>
        /// Obtiene los usuarios del grupo filtrado por los parametros indicados.
        /// </summary>
        ConsultaPaginada<Dominio.Seguridad.Modelos.UsuarioDeGrupoItem> ConsultarUsuarios(ConsultaUsuariosGrupo parametros, string subjectId);

        /// <summary>
        /// Obtiene un grupo por su id y si tiene permiso de lectura el usuario.
        /// </summary>
        Modelos.Grupo ObtenerGrupoPorLectura(int id, string subjectId);
    }
}
