using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Nucleo.Entidades
{
    /// <summary>
    /// Representa un elemento de los roles particulares del usuario.
    /// </summary>
    [Table("RolesParticulares", Schema = "Seguridad")]
    public class RolParticularVista : RolVistaBase { }
}