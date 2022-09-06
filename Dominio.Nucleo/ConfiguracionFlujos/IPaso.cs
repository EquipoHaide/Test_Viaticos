using System;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{
   
    public interface IPaso 
    {
        [JsonPropertyName("Id")]
        int Id { get; set; }

        [JsonPropertyName("Orden")]
        int Orden { get; set; }
        
        [JsonPropertyName("Rol")]
        int Rol { get; set; }

        [JsonPropertyName("TipoRol")]
        int TipoRol { get; set; }

        [JsonPropertyName("EsFirma")]
        bool EsFirma { get; set; }

        public bool IsValid();
    }
}
