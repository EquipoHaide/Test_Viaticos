using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TPaso>
          where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
        public const string TAG = "Dominio.Nucleo.Servicios.ServicioConfiguracionFlujoBase";

  
        public Respuesta<TFlujo> Crear(TFlujo flujo, bool esPredeterminado, bool esNivelRepetido, bool esEntePublico, string subjectId)
        {
            if (flujo == null)
                return new Respuesta<TFlujo>("Es requerido un flujo de autorizacion ", TAG);
          
            if (flujo?.IdEntePublico == null || flujo.IdEntePublico <= 0)
                return new Respuesta<TFlujo>("El Tipo de Ente es requerido", TAG);

            if (esEntePublico)
                return new Respuesta<TFlujo>("Ya se encuentra registrado flujos con este EntePublico", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta<TFlujo>("La lista de pasos es requerida.", TAG);

            if (flujo.TipoFlujo <= 0)
                return new Respuesta<TFlujo>("El tipo de flujo es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo?.IdNivelEmpleado == null || flujo.IdNivelEmpleado <= 0)
                    return new Respuesta<TFlujo>("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            if (esPredeterminado && flujo.TipoFlujo == (int)TipoFlujo.Predeterminado)
                return new Respuesta<TFlujo>("Solo se permite un flujo predeterminado ", TAG);

            if (!esPredeterminado && flujo.TipoFlujo != (int)TipoFlujo.Predeterminado)
                return new Respuesta<TFlujo>("Es necesario la creación de un flujo predeterminado", TAG);

            if (esNivelRepetido)
                return new Respuesta<TFlujo>("No se permite flujos con el mismo nivel de empleado", TAG);        
           
            foreach (var item in flujo.Pasos)
            {
                var respuestaPaso = this.ValidarPaso(item);

                if (!respuestaPaso.Contenido)
                    return new Respuesta<TFlujo>(respuestaPaso.Mensaje, TAG);
            }

            if (this.EsRepetido(flujo.Pasos))
                return new Respuesta<TFlujo>("La lista de pasos del flujo no deben de repetirse", TAG);

            if (!this.EsConsecutivo(flujo.Pasos))
                return new Respuesta<TFlujo>("La lista de pasos del flujo debe ser consecutivo.", TAG);

            return new Respuesta<TFlujo>(flujo);
        }


        public Respuesta<TFlujo> Modificar(TFlujo flujo, TFlujo flujoOriginal, bool esPredeterminado, bool esNivelRepetido, string subjectId)
        {
            if (flujo == null)
                return new Respuesta<TFlujo>("Es requerido un flujo de autorizacion ", TAG);

            if (flujoOriginal == null || flujoOriginal.Pasos == null)
                return new Respuesta<TFlujo>("El flujo no existe", TAG);

            if (flujoOriginal.TipoFlujo == (int)TipoFlujo.Predeterminado && flujo.TipoFlujo != (int)TipoFlujo.Predeterminado && esPredeterminado)
                return new Respuesta<TFlujo>("Es necesario que exista un flujo predeterminado, antes que un Particular", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta<TFlujo>("La lista de pasos es requerida.", TAG);

            if (flujo?.IdEntePublico == null || flujo.IdEntePublico <= 0)
                return new Respuesta<TFlujo>("El Tipo de Ente es requerido", TAG);

            if (flujo.TipoFlujo.ToString() == null)
                return new Respuesta<TFlujo>("El tipo de flujo es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo?.IdNivelEmpleado == null || flujo.IdNivelEmpleado <= 0)
                    return new Respuesta<TFlujo>("El nivel del empleado es requerido para un flujo particular.", TAG);

                if (!(flujoOriginal.IdNivelEmpleado == flujo.IdNivelEmpleado) && esNivelRepetido)
                    return new Respuesta<TFlujo>("No se permite flujos con el mismo nivel de empleado", TAG);
            }


            ////Aplicamos la validacion con respecto a los pasos.
            //foreach (var item in flujo.Pasos)
            //{
            //    var respuestaPaso = this.ValidarPaso(item);

            //    if (!respuestaPaso.Contenido)
            //        return new Respuesta<TFlujo>(respuestaPaso.Mensaje, TAG);
            //}

            //if (this.EsRepetido(flujo.Pasos))
            //    return new Respuesta<TFlujo>("La lista de pasos del flujo no deben de repetirse", TAG);

            if (!this.EsConsecutivo(flujo.Pasos))
                return new Respuesta<TFlujo>("La lista de pasos del flujo debe ser consecutivo.", TAG);

            flujoOriginal.IdEntePublico = flujo.IdEntePublico;
            flujoOriginal.TipoFlujo = flujo.TipoFlujo;


            //foreach (var item in flujoOriginal.Pasos)
            //{
            //    if(item.i)
            //    item.Orden = 
            //}

            foreach (var item in flujoOriginal.Pasos)
            {
                var flujoIdentico = flujo.Pasos.Where(r => r.Id == item.Id).FirstOrDefault();
                if(flujoIdentico != null)
                {
                    item.Orden = flujoIdentico.Orden;
                    item.EsFirma = flujoIdentico.EsFirma;

                }

            }
            //flujoOriginal.Pasos = flujo.Pasos;

            return new Respuesta<TFlujo>(flujoOriginal);
        }

        public Respuesta<TFlujo> Eliminar(TFlujo flujo, string subjectId)
        {
            if (flujo == null)
                return new Respuesta<TFlujo>("El flujo no existe",TAG);


            return new Respuesta<TFlujo>(flujo);
        }

        private Respuesta<bool> ValidarPaso(TPaso paso)
        {

            if (paso.Orden <= 0)
                return new Respuesta<bool>(String.Format("El orden del paso debe ser mayor a 0"), TAG);

            if (paso?.IdRol == null)
                return new Respuesta<bool>("El rol es requerido", TAG);

            if (paso?.TipoRol == null && paso.TipoRol <= 0)
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
