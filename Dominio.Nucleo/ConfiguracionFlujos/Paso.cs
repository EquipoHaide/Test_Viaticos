using System;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{
   
    public class Paso : IPaso
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Orden")]
        public int Orden { get; set; }
        
        [JsonPropertyName("Rol")]
        public int Rol { get; set; }

        [JsonPropertyName("TipoRol")]
        public int TipoRol { get; set; }

        [JsonPropertyName("EsFirma")]
        public bool EsFirma { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
