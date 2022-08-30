using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Seguridad.Servicios.R
{
    public class strings
    {
        public const string GrupoNulo = "El grupo no puede ser nulo";
        public const string GrupoNombreVacio = "Se debe asignar un nombre al grupo.";
        public const string GrupoNombreDuplicado = "El nombre \"{0}\" ya se encuentra registrado.";
        public const string UsuarioNoTienePermisos = "El usuario no cuenta con los permisos suficientes para realizar la acción correspondiente.";
        public const string ListaVacia = "Uno o más elementos han sido eliminados previamente, o no cuentas con los permisos suficientes para realizar esta operación.";
        public const string UsuarioInvalido = "El usuario no es válido.";
        public const string UsuarioNoExiste = "El usuario no existe.";
        public const string UsuarioAsignadoNoExiste = "El usuario Asignado al grupo no existe.";
        public const string UsuarioDeshabilitado = "El usuario se encuentra deshabilitado. Para mayor información comunicarse con el administrador de usuarios.";
        public const string SesionesPermitidasSuperadas = "El número de sesiones permitidas para el usuario han sido superadas.";
        public const string TokenInvalido = "El token de sesión no es válido.";
        public const string TokenExpirado = "El token de sesión ya ha expirado.";
        public const string SesionInvalida = "La sesión del usuario no es válida.";
        public const string SesionesPermitidasInvalidas = "La cantidad de las sesiones permitidas no es válida.";
        public const string RolNulo = "El rol no puede ser nulo";
        public const string RolNombreVacio = "Se debe asignar un nombre al rol.";
        public const string RolNombreDuplicado = "El nombre \"{0}\" ya se encuentra registrado.";
        public const string RolAdministradoInvalido = "Debe indicar un rol para administrar sus recursos.";
        public const string AccesoNulo = "El acceso no puede ser nulo";
        public const string FechaCaducidadInvalida = "La fecha de caducidad debe ser mayor a la fecha actual";


        public const string Value = "";
        public const string RegistroInactivo = "La acción no pudo concluirse, porque el registro fue previamente eliminado.";
        public const string RecursoRolInactivo = "No se puede realizar la acción, ya que el Rol \"{0}\" ha sido previamente eliminado.";
    }
}
