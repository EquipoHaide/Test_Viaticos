using System;

namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimientoEliminacion
    {
        string IdUsuarioElimino { get; set; }
        DateTime? FechaEliminacion { get; set; }
        bool Activo { get; set; }

        void Seguir(string idUsuario, bool esEliminar);
    }
}