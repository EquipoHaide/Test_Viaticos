
namespace Dominio.Nucleo
{
    public class PermisoBase : IPermiso
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public int IdRecurso { get; set; }
        public bool EsLectura { get; set; }
        public bool EsEscritura { get; set; }
        public bool EsEjecucion { get; set; }
    }
}