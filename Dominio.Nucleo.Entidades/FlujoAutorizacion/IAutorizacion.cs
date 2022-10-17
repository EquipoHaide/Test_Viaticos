using System;
namespace Dominio.Nucleo.Entidades
{
    public interface IAutorizacion : IEntity,IQuien
    {

        public int      IdFlujo { get; set; }
        public int      Orden { get; set; }
        public int      IdRol { get; set; }
        public string   Sello { get; set; }
        public int      Estado { get; set; }

     

    }
}
