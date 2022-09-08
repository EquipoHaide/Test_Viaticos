using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso>
    where TPaso : IPaso
    {
        bool ValidarPasos();
        public void Crear(List<IFlujo<TPaso>> flujos);
    }
}
