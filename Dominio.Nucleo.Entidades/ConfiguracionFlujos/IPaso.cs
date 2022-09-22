using System;

namespace Dominio.Nucleo.Entidades
{
    public interface IPaso : IEntity
    {
   
        int Orden { get; set; }
        int Rol { get; set; }
        int TipoRol { get; set; }
        bool EsFirma { get; set; }

    }
}
