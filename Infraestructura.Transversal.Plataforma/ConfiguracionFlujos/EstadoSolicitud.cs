using System;
using System.ComponentModel;

namespace Infraestructura.Transversal.Plataforma
{
    public enum EstadoSolicitud
    {
        [Description("Pendiente")]
        Pendiente = 1,
        [Description("Autorizado")]
        Autorizado = 2,
        [Description("Devuelto")]
        Devuelto = 3,
        [Description("DevueltoInicio")]
        DevueltoInicio = 4,
        [Description("Cancelado")]
        Cancelado = 5,

    }
}
