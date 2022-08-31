using System;
using System.ComponentModel;

namespace Infraestructura.Transversal.Plataforma
{
    public enum TipoFlujo
    {
        [Description("Predeterminado")]
        Predeterminado = 1,
        [Description("Particular")]
        Particular = 2,
    }
}
