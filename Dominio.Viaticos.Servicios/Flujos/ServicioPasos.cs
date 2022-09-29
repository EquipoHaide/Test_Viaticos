using System;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioPasos : ServicioPasoBas<Entidades.PasoViatico>, IServicioPasos<Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Viaticos.Servicios.Flujos";

        public Respuesta<PasoViatico> Eliminar(PasoViatico paso, bool validacionExtra, string subjectId)
        {
            return new Respuesta<PasoViatico>(paso);
        }

        public Respuesta<PasoViatico> Modificar(PasoViatico paso, PasoViatico pasoOriginal, bool validacionExtra, string subjectId)
        {
            return new Respuesta<PasoViatico>(paso);
        }
    }
}
