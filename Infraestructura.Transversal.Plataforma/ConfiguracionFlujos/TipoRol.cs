using System;
using System.ComponentModel;

namespace Infraestructura.Transversal.Plataforma
{
    public enum TipoRol
    {
        [Description("General")]
        General = 1,
        [Description("Especifico")]
        Especifico = 2,
    }
}
