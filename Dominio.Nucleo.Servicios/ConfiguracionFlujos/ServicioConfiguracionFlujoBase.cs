using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios
{
    public class ServicioConfiguracionFlujoBase<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo, TPaso>
        where TFlujo : IFlujo<TPaso>
        where TPaso  : IPaso
    {

        public const string TAG = "Dominio.Nucleo.Servicios.ServicioConfiguracionFlujoBase";

        public Respuesta<bool> ValidarFlujo(List<TFlujo> flujos)
        {

            //Valida que el objeto no este vacio
            if (flujos == null || flujos.Count < 1)
                return new Respuesta<bool>("Es requerido al menos un flujo de autorizacion ", TAG);

            if (flujos.Count(f => f.TipoFlujo == (int)TipoFlujo.Predeterminado) > 1)
                return new Respuesta<bool>("Solo se permite un flujo predeterminado ", TAG);


            foreach (var flujo in flujos)
            {
                //if (flujo.TipoEntePublico == null)
                //    return new Respuesta<bool>("El tipo ente publico es requerido", "TAG");

                //if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
                //{
                //    if (flujo.NivelEmpleado == null)
                //        return new Respuesta<bool>("El nivel del empleado es requerido para un flujo particular.", "TAG");
                //}

                //Aplicamos la validacion con respecto a los pasos.
                if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                    return new Respuesta<bool>("La lista de pasos es requerida.", "TAG");


                foreach (var item in flujo.Pasos)
                {
                    var respuestaPaso = this.ValidarPaso(item);

                    if (!respuestaPaso.Contenido)
                        return new Respuesta<bool>("La información de los pasos esta incompleta", "TAG");
                }

                if (this.EsRepetido(flujo.Pasos.Cast<IPaso>().ToList()))
                    return new Respuesta<bool>("La lista de pasos del flujo no deben de repetirse", TAG);

                if (!this.EsConsecutivo(flujo.Pasos.Cast<IPaso>().ToList()))
                    return new Respuesta<bool>("La lista de pasos del flujo debe ser consecutivo.", TAG);

            }

            return new Respuesta<bool>(true);
        }

        public Respuesta<bool> ValidarPaso(IPaso paso)
        {

            if (paso.Orden <= 0)
                return new Respuesta<bool>("El orden debe ser mayor a 0", "");

            if (paso.Rol <= 0)
                return new Respuesta<bool>("El rol es requerido", "");

            if (paso.TipoRol <= 0)
                return new Respuesta<bool>("El tipo rol es requerido", "");

            return new Respuesta<bool>(true);
        }


        private bool EsRepetido(List<IPaso> paso)
        {
            return paso.GroupBy(x => x.Orden).Any(g => g.Count() > 1);
        }

        private bool EsConsecutivo(List<IPaso> paso)
        {

            int index = 1;
            var pasosOrdenados = paso.OrderBy(x => x.Orden);
            foreach (var item in pasosOrdenados)
            {
                if (item.Orden != index)
                {
                    return false;
                }

                index++;
            }
            return true;
        }

    }
}
