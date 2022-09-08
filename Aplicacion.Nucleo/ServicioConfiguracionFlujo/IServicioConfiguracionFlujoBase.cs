using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso>
    where TPaso : IPaso
    {
        //bool ValidarPasos();
        public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos);
    }
}
