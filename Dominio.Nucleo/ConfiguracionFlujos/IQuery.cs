using System;
namespace Dominio.Nucleo
{
    public interface IQuery
    {
        public string Query { get; set; }

        public int Pagina { get; set; }

        public int ElementosPorPagina { get; set; }
    }
}
