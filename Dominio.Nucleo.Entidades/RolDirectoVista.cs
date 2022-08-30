using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa los elementos de la vista de roles directos del usuario
    /// </summary>
    [Table("RolesDirectos", Schema = "Seguridad")]
    public class RolDirectoVista : RolVistaBase { }
}