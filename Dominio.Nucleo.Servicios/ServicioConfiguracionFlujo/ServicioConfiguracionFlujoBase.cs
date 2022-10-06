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

  

        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, List<TFlujo> flujoOriginal, bool esPredeterminado, bool esEntePublico, string subjectId)
        {
            if (flujos.Count() < 0)
                return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion ", TAG);

            if (!esEntePublico)
                return new Respuesta<List<TFlujo>>("El Ente Publico Relacionado no existe", TAG);

            foreach (var flujo in flujos)
            {

                if (flujo.Id == 0)
                {
                    var respuesta = this.ValidarFlujo(flujo, esPredeterminado);
                    if (respuesta.EsError)
                        return new Respuesta<List<TFlujo>>(respuesta.Mensaje, TAG);

                    flujo.Seguir(subjectId);
                    flujoOriginal.Add(flujo);
                }

                if (flujo.Id > 0 && !flujo.Activo)
                {
                    var flujoEliminado = flujoOriginal.Where(r => r.Id == flujo.Id).FirstOrDefault();

                    flujoEliminado.Seguir(subjectId, false, true);
                }

                if (flujo.Id > 0)
                {
                    var flujoModificado = flujoOriginal.Where(r => r.Id == flujo.Id).FirstOrDefault();

                    flujoModificado = flujo;

                    flujoModificado.Seguir(subjectId, true, false);
                }

            }

            if(this.EsNivelRepetido(flujoOriginal))
                return new Respuesta<List<TFlujo>>("Ya existe un flujo con el mismo nivel de empleado", TAG);

            return new Respuesta<List<TFlujo>>(flujoOriginal);
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

            if (esPredeterminado && flujo.TipoFlujo == (int)TipoFlujo.Predeterminado)
                return new Respuesta("Solo se permite un flujo predeterminado ", TAG);

            if (!esPredeterminado && flujo.TipoFlujo != (int)TipoFlujo.Predeterminado)
                return new Respuesta("Es necesario la creación de un flujo predeterminado, antes de un particular", TAG);

            foreach (var item in flujo.Pasos)
            {
                var respuestaPaso = this.ValidarPaso(item);

                if (!respuestaPaso.Contenido)
                    return new Respuesta(respuestaPaso.Mensaje, TAG);
            }

            if (this.EsRepetido(flujo.Pasos))
                return new Respuesta("La lista de pasos del flujo no deben de repetirse", TAG);

            if (!this.EsConsecutivo(flujo.Pasos))
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



        //public Respuesta<List<TFlujo>> Crear(List<TFlujo> flujos, bool esPredeterminado, bool esEntePublico, string subjectId)
        //{
        //    if (flujos.Count() < 0)
        //        return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion ", TAG);

        //    if (!esEntePublico)
        //        return new Respuesta<List<TFlujo>>("El Ente Publico Relacionado no existe", TAG);

        //    if (this.EsNivelRepetido(flujos))
        //        return new Respuesta<List<TFlujo>>("Ya existe un flujo con el mismo nivel de empleado", TAG);

        //    var flujosNuevos = flujos.Where(r => r.Id == 0).ToList();

        //    foreach (var flujo in flujosNuevos)
        //    {
        //        if (flujo?.IdTipoEnte == null || flujo.IdTipoEnte <= 0)
        //            return new Respuesta<List<TFlujo>>("El Tipo de Ente es requerido", TAG);

        //        if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
        //            return new Respuesta<List<TFlujo>>("La lista de pasos es requerida.", TAG);


        //        if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
        //        {
        //            if (flujo?.IdNivelEmpleado == null || flujo.IdNivelEmpleado <= 0)
        //                return new Respuesta<List<TFlujo>>("El nivel del empleado es requerido para un flujo particular.", TAG);
        //        }

        //        if (esPredeterminado && flujo.TipoFlujo == (int)TipoFlujo.Predeterminado)
        //            return new Respuesta<List<TFlujo>>("Solo se permite un flujo predeterminado ", TAG);

        //        if (!esPredeterminado && flujo.TipoFlujo != (int)TipoFlujo.Predeterminado)
        //            return new Respuesta<List<TFlujo>>("Es necesario la creación de un flujo predeterminado, antes de un particular", TAG);

        //        foreach (var item in flujo.Pasos)
        //        {
        //            var respuestaPaso = this.ValidarPaso(item);

        //            if (!respuestaPaso.Contenido)
        //                return new Respuesta<List<TFlujo>>(respuestaPaso.Mensaje, TAG);
        //        }

        //        if (this.EsRepetido(flujo.Pasos))
        //            return new Respuesta<List<TFlujo>>("La lista de pasos del flujo no deben de repetirse", TAG);

        //        if (!this.EsConsecutivo(flujo.Pasos))
        //            return new Respuesta<List<TFlujo>>("La lista de pasos del flujo debe ser consecutivo.", TAG);
        //    }

        //    return new Respuesta<List<TFlujo>>(flujosNuevos);
        //}

        //public Respuesta<List<TFlujo>> Modificar(List<TFlujo> flujos, List<TFlujo> flujosOriginal, bool esPredeterminado, bool esNivelRepetido, string subjectId)
        //{
        //    if (flujos.Count() < 0)
        //        return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion ", TAG);


        //    foreach (var flujoModificado in flujos)
        //    {
        //        var flujoOriginal = flujosOriginal.Where(r => r.Id == flujoModificado.Id).FirstOrDefault();

        //        if (flujoOriginal == null)
        //            return new Respuesta<List<TFlujo>>("El flujo no existe", TAG);

        //        if (flujoModificado.Pasos.Count() <= 1)
        //            return new Respuesta<List<TFlujo>>("Es necesario que existe pasos en el flujo", TAG);

        //        if (flujoModificado?.IdTipoEnte == null || flujoModificado.IdTipoEnte <= 0)
        //            return new Respuesta<List<TFlujo>>("El Tipo de Ente es requerido", TAG);

        //        if (flujoOriginal.TipoFlujo == (int)TipoFlujo.Predeterminado
        //            && flujoModificado.TipoFlujo != (int)TipoFlujo.Predeterminado && esPredeterminado)
        //            return new Respuesta<List<TFlujo>>("Es necesario que exista un flujo predeterminado, antes que un Particular", TAG);

        //        if (flujoModificado.TipoFlujo == (int)TipoFlujo.Particular)
        //        {
        //            if (flujoModificado?.IdNivelEmpleado == null || flujoModificado.IdNivelEmpleado <= 0)
        //                return new Respuesta<List<TFlujo>>("El nivel del empleado es requerido para un flujo particular.", TAG);

        //            if (!(flujoOriginal.IdNivelEmpleado == flujoModificado.IdNivelEmpleado) && esNivelRepetido)
        //                return new Respuesta<List<TFlujo>>("No se permite flujos con el mismo nivel de empleado", TAG);
        //        }

        //        ////Aplicamos la validacion con respecto a los pasos.
        //        foreach (var item in flujoModificado.Pasos)
        //        {
        //            var respuestaPaso = this.ValidarPaso(item);

        //            if (!respuestaPaso.Contenido)
        //                return new Respuesta<List<TFlujo>>(respuestaPaso.Mensaje, TAG);
        //        }


        //        if (!this.EsConsecutivo(flujoModificado.Pasos))
        //            return new Respuesta<List<TFlujo>>("La lista de pasos del flujo debe ser consecutivo.", TAG);


        //        flujoOriginal.IdNivelEmpleado = flujoModificado.IdNivelEmpleado;
        //        flujoOriginal.Pasos = flujoModificado.Pasos;
        //    }


        //    //if (flujoOriginal == null || flujoOriginal.Pasos == null)
        //    //    return new Respuesta<List<TFlujo>>("El flujo no existe", TAG);

        //    //if (flujoOriginal.TipoAutorizacion == (int)TipoFlujo.Predeterminado && flujo.TipoAutorizacion != (int)TipoFlujo.Predeterminado && esPredeterminado)
        //    //    return new Respuesta<List<TFlujo>>("Es necesario que exista un flujo predeterminado, antes que un Particular", TAG);

        //    //if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
        //    //    return new Respuesta<List<TFlujo>>("La lista de pasos es requerida.", TAG);

        //    //if (flujo?.IdTipoEnte == null || flujo.IdTipoEnte <= 0)
        //    //    return new Respuesta<List<TFlujo>>("El Tipo de Ente es requerido", TAG);

        //    //if (flujo.TipoAutorizacion.ToString() == null)
        //    //    return new Respuesta<List<TFlujo>>("El tipo de flujo es requerido", TAG);

        //    //if (flujo.TipoAutorizacion == (int)TipoFlujo.Particular)
        //    //{
        //    //    if (flujo?.IdNivelEmpleado == null || flujo.IdNivelEmpleado <= 0)
        //    //        return new Respuesta<List<TFlujo>>("El nivel del empleado es requerido para un flujo particular.", TAG);

        //    //    if (!(flujoOriginal.IdNivelEmpleado == flujo.IdNivelEmpleado) && esNivelRepetido)
        //    //        return new Respuesta<List<TFlujo>>("No se permite flujos con el mismo nivel de empleado", TAG);
        //    //}


        //    ////Aplicamos la validacion con respecto a los pasos.
        //    //foreach (var item in flujo.Pasos)
        //    //{
        //    //    var respuestaPaso = this.ValidarPaso(item);

        //    //    if (!respuestaPaso.Contenido)
        //    //        return new Respuesta<TFlujo>(respuestaPaso.Mensaje, TAG);
        //    //}

        //    //if (this.EsRepetido(flujo.Pasos))
        //    //    return new Respuesta<TFlujo>("La lista de pasos del flujo no deben de repetirse", TAG);

        //    //if (!this.EsConsecutivo(flujo.Pasos))
        //    //    return new Respuesta<List<TFlujo>                  >("La lista de pasos del flujo debe ser consecutivo.", TAG);

        //    //flujoOriginal.IdTipoEnte = flujo.IdTipoEnte;
        //    //flujoOriginal.TipoAutorizacion = flujo.TipoAutorizacion;


        //    //foreach (var item in flujoOriginal.Pasos)
        //    //{
        //    //    if(item.i)
        //    //    item.Orden = 
        //    //}

        //    //foreach (var item in flujoOriginal.Pasos)
        //    //{
        //    //    var flujoIdentico = flujo.Pasos.Where(r => r.Id == item.Id).FirstOrDefault();
        //    //    if(flujoIdentico != null)
        //    //    {
        //    //        item.Orden = flujoIdentico.Orden;
        //    //        item.AplicaFirma = flujoIdentico.AplicaFirma;

        //    //    }

        //    //}
        //    //flujoOriginal.Pasos = flujo.Pasos;

        //    return new Respuesta<List<TFlujo>>(flujosOriginal);
        //}

        //public Respuesta<TFlujo> Eliminar(TFlujo flujo, string subjectId)
        //{
        //    if (flujo == null)
        //        return new Respuesta<TFlujo>("El flujo no existe", TAG);




        //    return new Respuesta<TFlujo>(flujo);
        //}

    }
}
