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


            foreach (var solicitud in instanciaCondensadas)
            {
                var autorizacion = autorizaciones.Where(r => r.Id == solicitud.IdAutorizacion).FirstOrDefault();

                if(autorizacion == null)
                    return new Respuesta("No se encontro autorizacion relacionada con la solicitud", TAG);


                //if(solicitud.IdRol == autorizacion.IdRol)
                //    return new Respuesta("La solicitud no puede ser autorizada ", TAG);

                var flujo = flujos.Where(r => r.Id == autorizacion.IdFlujo).FirstOrDefault();

                if (flujo == null)
                    return new Respuesta("No se encontro flujo relacionada con la solicitud", TAG);

                if (flujo.Pasos == null || (flujo.Pasos.Where(p => p.Activo).Count() <= 0))
                    return new Respuesta("El flujo debe tener por lo menos un paso.", TAG);

                var pasosOrdenados = flujo.Pasos.OrderBy(r => r.Orden);

                var pasoActual = pasosOrdenados.Where(r => r.Orden == solicitud.Orden).FirstOrDefault();

                var pasoSiguiente = pasosOrdenados.Where(r => r.Orden == solicitud.Orden + 1).FirstOrDefault();

                var pasoFinal = pasosOrdenados.LastOrDefault();


               



            }


            return new Respuesta();
        }



        private Respuesta AccionesSolicitud()
        {
            //if ((int)AccionSolicitud.Autorizado == accion) {

            //    if(item.Estado == (int)AccionSolicitud.Pendiente)
            //    {
            //        var nuevaAutorizacion = new TAutorizacion() {

            //            Orden = item.Orden + 1,
            //            //IdRol =
            //        };
            //    }

            //}else if ((int)AccionSolicitud.Devuelto == accion)
            //{

            //}else if ((int)AccionSolicitud.DevueltoInicio == accion)
            //{

            //}else if ((int)AccionSolicitud.Cancelado == accion)
            //{

            //}
            //else {

            //}
            return new Respuesta();
        }

     
    }
}
