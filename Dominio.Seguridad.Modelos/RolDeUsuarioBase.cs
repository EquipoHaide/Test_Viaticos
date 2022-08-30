using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class RolDeUsuarioBase : IRolItem
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool EsAsignado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
