using Dominio.Nucleo.Repositorios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;
using System.Collections.Generic;


namespace Dominio.Seguridad.Repositorios
{
    public interface IRepositorioAcciones : IRepository<Entidades.Accion>, IRepositorioRecurso<RecursoAccion>
    {
        /// <summary>
        /// Obtiene un acceso hacia la accion al cual el usuario tenga permiso.
        /// </summary>
        Entidades.Acceso ObtenerAcceso(string accion, string subjectId);

        /// <summary>
        /// Obtiene un acceso al cual el usuario tenga permiso.
        /// </summary>
        Entidades.Acceso ObtenerAcceso(int IdAcceso, string subjectId);

        List<Dominio.Seguridad.Entidades.Acceso> ObtenerAccesos(List<string> acciones, string subjectId);

        /// <summary>
        /// Obtiene los modulos con con opciones de modulo y acciones principales a las que puede acceder un usuario
        /// </summary>
        List<Modelos.Modulo> ObtenerModulos(string subjectId);

        ConsultaPaginada<Modelos.Acceso> ConsultarAccesos(ConsultaAcceso parametros, string subjectId);

        /// <summary>
        /// Obtiene los permisos que el usuario posee en sus roles de usuario sobre los Accesos Solicitados.
        /// </summary>
        IEnumerable<Entidades.RecursoAccion> ObtenerPermisosDeUsuarioAcciones(List<int> idsAcciones, string subjectId);

    }
}
