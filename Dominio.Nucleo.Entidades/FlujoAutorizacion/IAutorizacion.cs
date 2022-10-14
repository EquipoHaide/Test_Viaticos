using System;
namespace Dominio.Nucleo.Entidades
{
    public interface IAutorizacion : IEntity,ISeguimientoCreacion
    {

        public int      IdFlujo { get; set; }
        public int      Orden { get; set; }
        public int      IdRol { get; set; }
        public string   Sello { get; set; }
        public int      Estado { get; set; }
        public string   IdUsuarioAutorizacion { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public string   IdUsuarioCancelacion { get; set; }
        public DateTime FechaCancelacion { get; set; }

    }
}
