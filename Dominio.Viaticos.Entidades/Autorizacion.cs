using System;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    public class Autorizacion : IAutorizacion
    {
        
        public int IdFlujo { get; set ; }

        public int Orden { get ; set; }

        public int IdRol { get ; set; }

        public string QuienLoHizo { get ; set; }

        public DateTime FechaOperacion { get ; set; }

        public string Sello { get ; set; }

        public int Estado { get ; set; }

        public int Id { get ; set; }
    }
}
