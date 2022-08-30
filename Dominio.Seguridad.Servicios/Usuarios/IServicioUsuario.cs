using Dominio.Nucleo;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Seguridad.Servicios.Usuarios
{
    public interface IServicioUsuarios
    {
        /// <summary>
        /// Construye una sesión si el usuario indicado tiene permitido entrar en el sistema.
        /// </summary>
        Respuesta<Entidades.Sesion> IniciarSesion(Modelos.Usuario usuario, ref Entidades.Usuario usuarioLocal, string token, DateTime expirationToken);
        /// <summary>
        /// Actualiza la informacion de una sesión
        /// </summary>
        Respuesta<Entidades.Sesion> ActualizarSesion(Entidades.Sesion sesion, string token, DateTime expirationToken);
        /// <summary>
        /// Permite cerrar una sesión expirando la vigencia de su token.
        /// </summary>
        Respuesta<Entidades.Sesion> CerrarSesion(Entidades.Sesion sesion);
        /// <summary>
        /// Actualiza el estado de un usuario Habilita/Deshabilita
        /// </summary>
        Respuesta<bool> ActualizarEstado(Entidades.Usuario usuario, bool esHabilitado);
        /// <summary>
        /// Actualiza la cantidad de sesiones permitidas para un usuario.
        /// </summary>
        Respuesta<bool> ActualizarCantidadDeSesionesPermitidas(Entidades.Usuario usuario, int sesionesPermitidas);
        /// <summary>
        /// Permite administrar el agregado o eliminado de roles al usuario.
        /// </summary>
        Respuesta<List<RolUsuario>> AdministrarRoles(List<RolDeUsuarioBase> roles, IEnumerable<IPermiso> permisos, string subjectId);

        /// <summary>
        /// Método que valida un usuario según el subjectId dado.
        /// </summary>
        Respuesta<Entidades.Usuario> ObtenerUsuario(Entidades.Usuario usuario);
    }
}
