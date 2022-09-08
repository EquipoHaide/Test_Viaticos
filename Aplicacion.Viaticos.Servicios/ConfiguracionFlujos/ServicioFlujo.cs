using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo<TPaso> : ServicioConfiguracionFlujoBase<TPaso>, IServicioFlujo
        where TPaso : IPaso
    {
        public override bool ValidarPasos(List<IFlujo<TPaso>> flujos)
        {

            foreach (var f in flujos)
            {
                if (f.IsValid())
                    return false;
            }


            return true; 
        }
    }
}
