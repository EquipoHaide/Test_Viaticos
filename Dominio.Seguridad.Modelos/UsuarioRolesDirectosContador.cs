
namespace Dominio.Seguridad.Modelos
{
    public class UsuarioRolesDirectosContador
    {
        public int IdUsuario { get; set; }
        public string SubjectId { get; set; }
        public int Roles { get; set; }
        public bool EsNuevo { get; set; }
    }
}
