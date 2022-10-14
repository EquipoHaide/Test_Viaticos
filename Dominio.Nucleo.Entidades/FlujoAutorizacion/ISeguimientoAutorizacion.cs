using System;
namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimientoAutorizacion
    {
        public string IdUsuarioAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }

        void Autorizar(string idUsuario);
    }
}
