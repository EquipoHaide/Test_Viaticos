using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase<Dominio.Viaticos.Entidades.FlujoViaticos, Dominio.Viaticos.Entidades.PasoViatico>,
        IServicioFlujos<Dominio.Viaticos.Entidades.FlujoViaticos, Dominio.Viaticos.Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        //public void Consultar(ConsultaConfiguracionFlujo parametros, string subjectId)
        //{
          
        //    throw new NotImplementedException();
        //}

        //public void Crear(FlujoViaticos flujos, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Modificar(FlujoViaticos flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
