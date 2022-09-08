using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloPaso : IPaso
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Rol { get; set; }
        public int TipoRol { get; set; }
        public bool EsFirma { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
