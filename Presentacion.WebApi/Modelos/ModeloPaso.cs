using System;
using Dominio.Nucleo.Entidades;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloPaso<TPaso>
        where TPaso : class,IPaso
    {


        public TPaso Paso { get; set; }
    }
}
