using System;
namespace Dominio.Nucleo.Entidades
{
    public interface IQuien
    {
        public string IdUsuarioAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public string IdUsuarioCancelacion { get; set; }
        public DateTime FechaCancelacion { get; set; }
    }
}
