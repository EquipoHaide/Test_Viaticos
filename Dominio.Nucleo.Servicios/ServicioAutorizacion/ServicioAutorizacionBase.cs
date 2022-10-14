using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso> : IServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion,new()
        where TInstanciaCondensada : class, ISolicitudCondensada
    {

        const string TAG = "Dominio.Nucleo.Servicios.ServicioAutorizacion";

        public Respuesta AdministrarAutorizacion(List<TInstanciaCondensada> instanciaCondensadas, List<TAutorizacion> autorizaciones, List<TFlujo> flujos, int accion, string subjectId)
        {
            if (autorizaciones == null || autorizaciones.Count() <= 0)
                    return new Respuesta("",TAG);

            if (flujos == null || flujos.Count() <= 0)
                return new Respuesta("", TAG);

            if (accion <= 0)
                return new Respuesta("Es necesario que especifique una Acción", TAG);


            foreach (var item in instanciaCondensadas)
            {
                var autorizacion = autorizaciones.Where(r => r.Id == item.IdAutorizacion).FirstOrDefault();

                if(autorizacion == null)
                    return new Respuesta("No se encontro autorizacion relacionada con la solicitud", TAG);

                var flujo = flujos.Where(r => r.Id == autorizacion.IdFlujo).FirstOrDefault();

                if (flujo == null)
                    return new Respuesta("No se encontro flujo relacionada con la solicitud", TAG);


                if ((int)EstadoSolicitud.Autorizado == accion) {

                    if(item.Estado == (int)EstadoSolicitud.Pendiente)
                    {

                    }

                }else if ((int)EstadoSolicitud.Devuelto == accion)
                {

                }else if ((int)EstadoSolicitud.DevueltoInicio == accion)
                {

                }else if ((int)EstadoSolicitud.Cancelado == accion)
                {

                }
                else {

                }



            }


            return new Respuesta();
        }


     
    }
}
