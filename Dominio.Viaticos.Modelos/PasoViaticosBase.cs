using System;
using System.Text.Json.Serialization;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class PasoViaticoBase : Dominio.Nucleo.Paso, IPasoModel
    {
        public string Descripcion { get; set; }
   
    }
}
