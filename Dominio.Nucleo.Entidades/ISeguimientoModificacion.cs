using System;

namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimientoModificacion
    {
        string IdUsuarioModifico { get; set; }
        DateTime FechaModificacion { get; set; }

        void Seguir(string idUsuario, bool esModificar = false);
    }
}