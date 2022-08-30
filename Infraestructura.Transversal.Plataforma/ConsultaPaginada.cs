using System;
using System.Collections.Generic;

namespace Infraestructura.Transversal.Plataforma
{
    public class ConsultaPaginada<TResult>
    {
        public int? Pagina { get; set; }
        public int? ElementosPorPagina { get; set; }
        public int TotalPaginas
        {
            get
            {
                if (TotalElementos == 0 && ElementosPorPagina == 0)
                    return 0;

                if (TotalElementos < ElementosPorPagina)
                    return 1;

                return (int)(Math.Ceiling((double)TotalElementos / (double)ElementosPorPagina));
            }
        }
        public int TotalElementos { get; set; }
        public IEnumerable<TResult> Elementos { get; set; }

        public ConsultaPaginada()
        {
            Pagina = 1;
            ElementosPorPagina = 20;
            TotalElementos = 0;
            Elementos = new List<TResult>();
        }

        public ConsultaPaginada(int pagina, int elementosPorPagina, int totalElementos, List<TResult> elementos)
        {
            Pagina = pagina;
            ElementosPorPagina = elementosPorPagina;
            TotalElementos = totalElementos;
            Elementos = elementos;
        }
    }
}