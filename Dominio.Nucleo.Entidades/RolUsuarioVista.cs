using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa un elemento de la vista de roles de usuario.
    /// </summary>
    [Table("RolesUsuario", Schema = "Seguridad")]
    public class RolUsuarioVista : RolVistaBase { }
}