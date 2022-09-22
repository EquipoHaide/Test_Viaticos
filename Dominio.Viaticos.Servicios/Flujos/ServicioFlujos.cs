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

        public Respuesta<FlujoViaticos> Crear(FlujoViaticos flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViaticos>(flujo);
        }

        public Respuesta<FlujoViaticos> Eliminar(FlujoViaticos flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViaticos>(flujo);
        }

        public Respuesta<FlujoViaticos> Modificar(FlujoViaticos flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViaticos>(flujo);

        }
    }
}
