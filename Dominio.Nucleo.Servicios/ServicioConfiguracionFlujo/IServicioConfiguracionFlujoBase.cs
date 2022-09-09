using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso> where TPaso : IPaso
    {

        public Respuesta<IFlujo<TPaso>> Crear(IFlujo<TPaso> flujos, bool esPredeterminado);

    }
}
