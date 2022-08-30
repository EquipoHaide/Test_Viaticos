using System;
using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class IEnumerableExtensions
    {
        public static ConsultaPaginada<TResult> Paginar<TResult>(this IEnumerable<TResult> list, int pagina = 1, int elementos = 20)
        {
            ConsultaPaginada<TResult> result = new ConsultaPaginada<TResult>(pagina, elementos, list.Count(), list.Skip((pagina - 1) * elementos).Take(elementos).ToList());

            return result;
        }
    }
}
