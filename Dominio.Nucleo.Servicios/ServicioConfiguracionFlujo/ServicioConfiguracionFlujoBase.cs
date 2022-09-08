using System;
using System.Collections.Generic;
using System.Linq;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public class ServicioConfiguracionFlujoBase<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
    {
        public const string TAG = "Dominio.Nucleo.Servicios.ServicioConfiguracionFlujoBase";

        public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos)
        {

            if (flujos.Count(f => f.TipoFlujo == (int)TipoFlujo.Predeterminado) > 1)
                return new Respuesta<bool>("Solo se permite un flujo predeterminado ", TAG);

            foreach (var flujo in flujos)
            {
                if (flujo.TipoEntePublico.Descripcion == null)
                    return new Respuesta<bool>("La descripcion del ente publico es requerido", TAG);

                if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
                {
                    if (flujo.NivelEmpleado.Nivel.ToString().IsNullOrEmptyOrWhiteSpace())
                        return new Respuesta<bool>("El nivel del empleado es requerido para un flujo particular.", TAG);
                }

                //Aplicamos la validacion con respecto a los pasos.
                
                foreach (var item in flujo.Pasos)
                {
                    var respuestaPaso = this.ValidarPaso(item);

                    if (!respuestaPaso.Contenido)
                        return new Respuesta<bool>(respuestaPaso.Mensaje, TAG);
                }

                if (this.EsRepetido(flujo.Pasos))
                    return new Respuesta<bool>("La lista de pasos del flujo no deben de repetirse", TAG);

                if (!this.EsConsecutivo(flujo.Pasos))
                    return new Respuesta<bool>("La lista de pasos del flujo debe ser consecutivo.", TAG);

            }

            return new Respuesta<bool>(true);
        }


        private Respuesta<bool> ValidarPaso(TPaso paso)
        {

            if (paso.Orden <= 0)
                return new Respuesta<bool>("El orden debe ser mayor a 0", TAG);

            if (paso.Rol <= 0)
                return new Respuesta<bool>("El rol es requerido", TAG);

            if (paso.TipoRol <= 0)
                return new Respuesta<bool>("El tipo rol es requerido", TAG);

            return new Respuesta<bool>(true);
        }


        private bool EsRepetido(List<TPaso> paso)
        {
            return paso.GroupBy(x => x.Orden).Any(g => g.Count() > 1);
        }

        private bool EsConsecutivo(List<TPaso> paso)
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
