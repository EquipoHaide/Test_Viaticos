using System;
namespace Dominio.Nucleo.Entidades
{
    public interface IAutorizacion : IEntity
    {

        public int      IdFlujo { get; set; }
        public int      Orden { get; set; }
        public int      IdRol { get; set; }
        public string   QuienLoHizo { get; set; }
        public DateTime FechaOperacion { get; set; }
        public string   Sello { get; set; }
        public int      Estado { get; set; }

    }
}
