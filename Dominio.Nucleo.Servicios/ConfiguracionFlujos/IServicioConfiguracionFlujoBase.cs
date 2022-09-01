using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
    {
        Respuesta<bool> ValidarFlujo(List<TFlujo> flujos);

        Respuesta<bool> ValidarPaso(TPaso paso);

    }
}
