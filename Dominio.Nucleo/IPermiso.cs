
namespace Dominio.Nucleo
{
    /// <summary>
    /// Define como sera interpretado un permiso y los atributos que posee.
    /// </summary>
    public interface IPermiso
    {
        int Id { get; set; }
        int IdRol { get; set; }
        int IdRecurso { get; set; }
        bool EsLectura { get; set; }
        bool EsEscritura { get; set; }
        bool EsEjecucion { get; set; }
    }
}