using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : IFlujo<TPaso>
        where TPaso  : IPaso
       
    {
        /// <summary>
        /// Validaciones adicionales de la creacion de un flujo 
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        Respuesta<bool> ValidarFlujo(List<TFlujo> flujos);

        //Respuesta<bool> ValidarPaso(TPaso paso);

    }
}
