using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    public interface IServicioConfiguracionFlujoBase
    {
        Respuesta<bool> ValidarFlujo(List<IFlujo> flujos);

        Respuesta<bool> ValidarPaso(IPaso paso);

    }
}
