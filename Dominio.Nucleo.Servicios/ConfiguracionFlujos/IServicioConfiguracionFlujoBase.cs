using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : IFlujo<TPaso>
        where TPaso  : IPaso
       
    {
        Respuesta<bool> ValidarFlujo(List<TFlujo> flujos);

        //Respuesta<bool> ValidarPaso(TPaso paso);

    }
}
