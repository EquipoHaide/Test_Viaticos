using System;
using System.Text.Json.Serialization;

namespace Dominio.Nucleo
{
   
    public interface IPaso 
    {
        int Id { get; set; }       
        int Orden { get; set; }    
        int Rol { get; set; }
        int TipoRol { get; set; }   
        bool EsFirma { get; set; }

    }
}
