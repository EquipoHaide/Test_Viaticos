using System;

namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimientoCreacion
    {
        string IdUsuarioCreo { get; set; }
        DateTime FechaCreacion { get; set; }

        void Seguir(string idUsuario);
    }
}