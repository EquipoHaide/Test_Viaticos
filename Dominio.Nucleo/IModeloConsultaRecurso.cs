
namespace Dominio.Nucleo
{
    /// <summary>
    /// Representa el modelo base para realizar la busqueda de recursos para la consulta de la administración de los recursos protegidos.
    /// </summary>
    public interface IModeloConsultaRecurso : IModel
    {
        /// <summary>
        /// Id del rol que esta siendo administrado
        /// </summary>
        int IdRol { get; set; }
        /// <summary>
        /// Parametro basico de busqueda
        /// </summary>
        string Query { get; set; }
        /// <summary>
        /// Pagina obtenida del resultado.
        /// </summary>
        int Pagina { get; set; }
        /// <summary>
        /// Cantidad de elementos por pagina que se obtendran.
        /// </summary>
        int ElementosPorPagina { get; set; }
    }
}