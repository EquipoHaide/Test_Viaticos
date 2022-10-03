using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public abstract class ServicioPasoBas<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {

        const string TAG = "Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo";

        public abstract Respuesta ReordenarPosicionesPorEliminacion(List<TPaso> listaPaso,string subjectId);

        public Respuesta<TPaso> Eliminar(TPaso paso,string subjectId)
        {
            if (paso == null)
                return new Respuesta<TPaso>("Es requerido un paso", TAG);

            if(paso.Id <= 0)
                return new Respuesta<TPaso>("El paso ya no existe", TAG);


            return new Respuesta<TPaso>(paso);
        }

        public Respuesta<TPaso> Modificar(TPaso paso, TPaso pasoOriginal, string subjectId)
        {
            if(paso == null)
                return new Respuesta<TPaso>("Es requerido un paso", TAG);

            if(paso.IdConfiguracionFlujo <= 0)
                return new Respuesta<TPaso>("El paso no tiene ninguna vinculación con un flujo", TAG);

            if(pasoOriginal.Id <= 0 || pasoOriginal == null)
                return new Respuesta<TPaso>("El paso no existe", TAG);

            if (paso?.IdRol == null)
                return new Respuesta<TPaso>("El rol es requerido", TAG);

            if (paso?.TipoRol == null && paso.TipoRol <= 0)
                return new Respuesta<TPaso>("El tipo rol es requerido", TAG);
            if (paso?.EsFirma == null)
                return new Respuesta<TPaso>("El tipo rol es requerido", TAG);


            pasoOriginal.IdRol = paso.IdRol;
            pasoOriginal.TipoRol = paso.TipoRol;
            pasoOriginal.EsFirma = paso.EsFirma;


            return new Respuesta<TPaso>(pasoOriginal);
        }
    }
}
