using System;
using System.Text.Json.Serialization;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class PasoViatico : PasoB
    {
      public string Descripcion { get; set; }
    }
}
