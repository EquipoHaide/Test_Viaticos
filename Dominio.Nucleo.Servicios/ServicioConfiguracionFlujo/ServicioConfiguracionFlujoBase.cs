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

        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, List<TFlujo> flujosOriginales, bool esPredeterminado, bool esEntePublico, string subjectId)
        {
            //if (flujos.Count() < 0)
            //    return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion ", TAG);

            if (!esEntePublico)
                return new Respuesta<List<TFlujo>>("El Ente Publico Relacionado no existe", TAG);

            
            foreach (var itemFlujo in flujos)
            {

                if (itemFlujo.Id == 0)
                {

                    var flujoNuevo = this.AdministrarFlujo(itemFlujo,null, esPredeterminado, subjectId);

                    if(flujoNuevo.EsError)
                        return new Respuesta<List<TFlujo>>(flujoNuevo.Mensaje, TAG);

                    flujoNuevo.Contenido.Seguir(subjectId);

                    //itemFlujo.Seguir(subjectId);
                   
                    flujosOriginales.Add(flujoNuevo.Contenido);
             
                }
                else
                {
                    var flujoOriginal = flujosOriginales.Where(r => r.Id == itemFlujo.Id).FirstOrDefault();

                    if (flujoOriginal != null && flujoOriginal.TipoFlujo != (int)TipoFlujo.Particular)
                    {
                        if (esPredeterminado && itemFlujo.TipoFlujo != (int)TipoFlujo.Predeterminado)
                            return new Respuesta<List<TFlujo>>("No se puedo cambiar un flujo predeterminado a uno particular", TAG);
                    }

                    if (itemFlujo.Id > 0 && !itemFlujo.Activo)
                    {
                        flujoOriginal.Seguir(subjectId, false, true);
                    }

                    if(itemFlujo.Id > 0)
                    {

                        var flujoModificado = this.AdministrarFlujo(itemFlujo, flujoOriginal,esPredeterminado, subjectId);

                        if (flujoModificado.EsError)
                            return new Respuesta<List<TFlujo>>(flujoModificado.Mensaje, TAG);

                        flujoOriginal = flujoModificado.Contenido;
                        flujoOriginal.Seguir(subjectId, true, false);
                    }
                }
            }

            if(this.EsNivelRepetido(flujosOriginales))
                return new Respuesta<List<TFlujo>>("Ya existe un flujo con el mismo nivel de empleado", TAG);

            return new Respuesta<List<TFlujo>>(flujosOriginales);
        }



        private Respuesta<TFlujo> AdministrarFlujo(TFlujo flujo, TFlujo flujoOriginal, bool esPredeterminado, string subjectId)
        {

            //TFlujo flujo = flujoOriginal != null ? flujoOriginal = flujo : flujo;

            var respuesta = this.ValidarFlujo(flujo, esPredeterminado);

            if (respuesta.EsError)
                return new Respuesta<TFlujo>(respuesta.Mensaje, TAG);

            List<TPaso> listaPasos = flujoOriginal != null ? flujoOriginal.Pasos : new List<TPaso>();

            var administraPaso = this.AdministrarPasos(flujo.Pasos, listaPasos,subjectId);

            //flujoOriginal = flujo;  
            flujo.Pasos = administraPaso;
            
            return new Respuesta<TFlujo>(flujo);

        }


        private List<TPaso> AdministrarPasos(List<TPaso> listaPasos, List<TPaso> pasosOriginales, string subjectId)
        {

            foreach (var itemPaso in listaPasos)
            {
                if (itemPaso.Id == 0)
                {
                    itemPaso.Seguir(subjectId);
                    pasosOriginales.Add(itemPaso);
                }
                   

                var paso = pasosOriginales.Where(r => r.Id == itemPaso.Id).FirstOrDefault();

                if (itemPaso.Id > 0 && !itemPaso.Activo)
                {
                    paso.Seguir(subjectId, false, true);
                }
                

                if (itemPaso.Id > 0) {

                    paso.IdRolAutoriza = itemPaso.IdRolAutoriza;
                    paso.TipoRol = itemPaso.TipoRol;
                    paso.Orden = itemPaso.Orden;
                    paso.AplicaFirma = itemPaso.AplicaFirma;

                    paso.Seguir(subjectId, true, false);
                }
                    
            }

            return pasosOriginales;
        }

        private Respuesta ValidarFlujo(TFlujo flujo, bool esPredeterminado)
        {
            if (flujo?.IdTipoEnte == null || flujo.IdTipoEnte <= 0)
                return new Respuesta("El Tipo de Ente es requerido", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta("La lista de pasos es requerida.", TAG);


            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo?.IdNivelEmpleado == null || flujo.IdNivelEmpleado <= 0)
                    return new Respuesta("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            if (esPredeterminado && flujo.TipoFlujo == (int)TipoFlujo.Predeterminado && flujo.Id == 0)
                return new Respuesta("Solo se permite un flujo predeterminado ", TAG);

            if (!esPredeterminado && flujo.TipoFlujo != (int)TipoFlujo.Predeterminado)
                return new Respuesta("Es necesario la creación de un flujo predeterminado, antes de un particular", TAG);

            var listaPasos = flujo.Pasos.Where(x => x.Activo).ToList();

            foreach (var item in listaPasos)
            {
                var respuestaPaso = this.ValidarPaso(item);

                if (!respuestaPaso.Contenido)
                    return new Respuesta(respuestaPaso.Mensaje, TAG);
            }

            if (this.EsRepetido(listaPasos))
                return new Respuesta("La lista de pasos del flujo no deben de repetirse", TAG);

            if (!this.EsConsecutivo(listaPasos))
                return new Respuesta("La lista de pasos del flujo debe ser consecutivo.", TAG);

            return new Respuesta();
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

        private bool EsNivelRepetido(List<TFlujo> flujos)
        {
            return flujos.GroupBy(x => x.IdNivelEmpleado).Any(g => g.Count() > 1);
        }

        private Respuesta<bool> ValidarPaso(TPaso paso)
        {

            if (paso.Orden <= 0)
                return new Respuesta<bool>(String.Format("El orden del paso debe ser mayor a 0"), TAG);

            if (paso?.IdRolAutoriza == null)
                return new Respuesta<bool>("El rol es requerido", TAG);

            if (paso?.TipoRol == null && paso.TipoRol <= 0)
                return new Respuesta<bool>("El tipo rol es requerido", TAG);

            return new Respuesta<bool>(true);
        }

    }
}
