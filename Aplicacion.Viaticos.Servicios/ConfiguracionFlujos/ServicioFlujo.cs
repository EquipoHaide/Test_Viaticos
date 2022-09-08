using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo<TPaso> : ServicioConfiguracionFlujoBase<TPaso>, IServicioFlujo<TPaso>
        where TPaso : IPaso
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }

        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

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
