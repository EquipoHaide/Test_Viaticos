using System;
namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimientoCancelacion
    {
        public string IdUsuarioCancelacion { get; set; }
        public DateTime FechaCancelacion { get; set; }

        void Cancelar(string idUsuario);
    }
}
