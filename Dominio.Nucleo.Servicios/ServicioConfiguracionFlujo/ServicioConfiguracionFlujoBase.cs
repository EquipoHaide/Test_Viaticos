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


       
        public Respuesta<TFlujo> Crear(TFlujo flujo, bool esPredeterminado, bool esNivelRepetido, string subjectId)
        {
            ///VALIDAR QUE SOLO EXISTA UN FLUJO PREDETERMINADO 
            if (esPredeterminado && flujo.TipoFlujo == (int)TipoFlujo.Predeterminado)
                return new Respuesta<TFlujo>("Solo se permite un flujo predeterminado ", TAG);

            ///VALIDAR QUE SOLO EXISTA UN FLUJO CON UN UNICO NIVEL DE EMPLEADO 
            if (esNivelRepetido && flujo.TipoFlujo == (int)TipoFlujo.Particular)
                return new Respuesta<TFlujo>("No se permite flujos con el mismo nivel de empleado", TAG);


            if (flujo.TipoEntePublico == null || flujo.TipoEntePublico.Id <=0)
                return new Respuesta<TFlujo>("El tipo ente publico es requerida", TAG);


            if (flujo.TipoEntePublico.Descripcion == null)
                return new Respuesta<TFlujo>("La descripcion del ente publico es requerido", TAG);

           
            //Aplicamos la validacion con respecto a los pasos.
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

            ////AQUI VA IR LA TRANSFORMACION A ENTITY
            /////EL MODELO A REGRESAR DEBE SER LA ENTITIDA BASE 

  


            return new Respuesta<TFlujo>(flujo);
        }


        public Respuesta<TFlujo> Modificar(TFlujo flujo, bool esPredeterminado, bool esNivelRepetido, string subjectId)
        {

            ///VALIDAR QUE SOLO EXISTA UN FLUJO PREDETERMINADO 
            if (esPredeterminado)
                return new Respuesta<TFlujo>("Solo se permite un flujo predeterminado ", TAG);

            ///VALIDAR QUE SOLO EXISTA UN FLUJO CON UN UNICO NIVEL DE EMPLEADO 
            if (esNivelRepetido)
                return new Respuesta<TFlujo>("No se permite flujos con el mismo nivel de empleado", TAG);

            if (flujo.TipoEntePublico == null)
                return new Respuesta<TFlujo>("El tipo ente publico es requerida", TAG);

            if (flujo.TipoEntePublico.Descripcion == null)
                return new Respuesta<TFlujo>("La descripcion del ente publico es requerido", TAG);


            //Aplicamos la validacion con respecto a los pasos.
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

            ////AQUI VA IR LA TRANSFORMACION A ENTITY
            /////EL MODELO A REGRESAR DEBE SER LA ENTITIDA BASE 

            return new Respuesta<TFlujo>(flujo);
        }

        private Respuesta<bool> ValidarPaso(TPaso paso)
        {

            if (paso.Orden <= 0)
                return new Respuesta<bool>(String.Format("El orden del paso con rol {0} debe ser mayor a 0", paso.Rol), TAG);

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

        public Respuesta<TFlujo>Eliminar(TFlujo flujo, string subjectId)
        {

            if (flujo == null)
                return new Respuesta<TFlujo>("El flujo no existe");

            

            return new Respuesta<TFlujo>(flujo);
        }
    }
}
