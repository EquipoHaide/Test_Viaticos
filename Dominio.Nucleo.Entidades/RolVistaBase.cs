
namespace Dominio.Nucleo.Entidades
{
    public class RolVistaBase : IRolItem
    {
        public int Id { get; set; }
        public string SubjectId { get; set; }
        public int IdRol { get; set; }
    }
}