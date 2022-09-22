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
    public class ServicioFlujos : ServicioConfiguracionFlujoBase<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico>,
        IServicioFlujos<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public Respuesta<FlujoViatico> Crear(FlujoViatico flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViatico>(flujo);
        }

        public Respuesta<FlujoViatico> Eliminar(FlujoViatico flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViatico>(flujo);
        }

        public Respuesta<FlujoViatico> Modificar(FlujoViatico flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<FlujoViatico>(flujo);

        }
    }
}
