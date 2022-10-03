using System;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using System.Collections.Generic;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioPasos : ServicioPasoBas<Entidades.PasoViatico>, IServicioPasos<Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Viaticos.Servicios.Flujos";

        public Respuesta<PasoViatico> Eliminar(PasoViatico paso, bool validacionExtra, string subjectId)
        {
            if (validacionExtra)
                return new Respuesta<PasoViatico>("La validación extra", TAG);

            paso.Seguir(subjectId, true);

            return new Respuesta<PasoViatico>(paso);
        }

        public Respuesta<PasoViatico> Modificar(PasoViatico paso, PasoViatico pasoOriginal, bool validacionExtra, string subjectId)
        {
            if(validacionExtra)
                return new Respuesta<PasoViatico>("La validación extra",TAG);

            pasoOriginal.IdRol = paso.IdRol;
            pasoOriginal.TipoRol = paso.TipoRol;
            pasoOriginal.EsFirma = paso.EsFirma;
            pasoOriginal.Descripcion = paso.Descripcion;

            pasoOriginal.Seguir(subjectId, true, false);

            return new Respuesta<PasoViatico>(pasoOriginal);

        }

       
        public override Respuesta ReordenarPosicionesPorEliminacion(List<PasoViatico> listaPaso, string subjectId)
        {
            var count = 1;

            foreach (var cp in listaPaso)
            {
                if (cp.Orden != count)
                {
                    cp.Orden = count;

                    cp.Seguir(subjectId, true, false);

                }

                count = count + 1;
            }


            return new Respuesta();
        }
    }
}
