using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class RolDeGrupoBase : IRolItem
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public int IdRol { get; set; }
        public bool EsAsignado { get; set; }
    }
}
