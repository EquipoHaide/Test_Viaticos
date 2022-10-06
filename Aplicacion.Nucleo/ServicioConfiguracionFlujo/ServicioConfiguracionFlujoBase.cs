using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Entidades = Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;

using Infraestructura.Transversal.Plataforma.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TFlujo, TPaso, TQuery> : IServicioConfiguracionFlujoBase<TFlujo, TPaso, TQuery>
        where TFlujo : class, Entidades.IFlujo<TPaso> 
        where TPaso : class, Entidades.IPaso
        where TQuery : class, IConsultaFlujo
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TFlujo,TPaso> ServicioDominio { get; }


        public virtual IRepositorioConfiguracionFlujo<TFlujo,TQuery> Repositorio { get; }
        
   
        public abstract Respuesta<List<TFlujo>> AdministrarConfiguracionFlujos(List<TFlujo> flujos, string subjectId);

        public Respuesta<ConsultaPaginada<TFlujo>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<TFlujo>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarFlujosDeAutorizacion(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<TFlujo>>(TAG);

            return new Respuesta<ConsultaPaginada<TFlujo>>(recursos.Contenido);
        }

   

        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujos.Count() <= 0)
                return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion", TAG);

            var esPredeterminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujos[0].IdTipoEnte));

            if (esPredeterminado.EsError)
                return new Respuesta<List<TFlujo>>(esPredeterminado.Mensaje,TAG);

            var flujoOriginal = Repositorio.Try(r => r.ObtenerFlujosPorEntePublico(flujos[0].IdTipoEnte));

            if (flujoOriginal.EsError)
                return new Respuesta<List<TFlujo>>(flujoOriginal.Mensaje, TAG);

            var esEntePublico = Repositorio.Try(r => r.ExisteRegistroEntePublico(flujos[0]));

            if (esEntePublico.EsError)
                return new Respuesta<List<TFlujo>>(esEntePublico.Mensaje, TAG);


            var respuesta = ServicioDominio.AdministrarFlujos(flujos, flujoOriginal.Contenido, esPredeterminado.Contenido, esEntePublico.Contenido, subjectId);


            if (respuesta.EsExito)
            {
                var respuestaComplementaria = this.AdministrarConfiguracionFlujos(respuesta.Contenido, subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    Repositorio.Add(respuestaComplementaria.Contenido);

                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                    {
                        return new Respuesta<List<TFlujo>>(save.Mensaje,TAG);
                    }

                    return new Respuesta<List<TFlujo>>(respuestaComplementaria.Contenido);
                }

                return new Respuesta<List<TFlujo>>(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }
            
            return new Respuesta<List<TFlujo>>(respuesta.Mensaje,TAG);
        }


        //public Respuesta Crear(List<TFlujo> flujo, string subjectId)
        //{
        //    //Valida que el objeto no este vacio
        //    if (flujo.Count() <= 0)
        //        return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

        //    var esPredeterminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo[0].IdTipoEnte));

        //    if (esPredeterminado.EsError)
        //        return esPredeterminado.ErrorBaseDatos(TAG);

        //    var flujoOriginal = Repositorio.Try(r => r.ObtenerFlujosPorEntePublico(flujo[0].IdTipoEnte));

        //    if (flujoOriginal.EsError)
        //        return flujoOriginal.ErrorBaseDatos(TAG);

        //    flujo.ForEach(flujo => {
        //        flujoOriginal.Contenido.Add(flujo);
        //    });

        //    var esEntePublico = Repositorio.Try(r => r.ExisteRegistroEntePublico(flujo[0]));

        //    ///PREGUNTA: SE TENDRIA QUE VALIRDAR LA EXISTENCIA DEL NIVEL DEL EMPLEADO 

        //    var respuesta = ServicioDominio.Crear(flujoOriginal.Contenido, esPredeterminado.Contenido, esEntePublico.Contenido, subjectId);

        //    if (respuesta.EsExito)
        //    {
        //        var respuestaComplementaria = this.CreacionFlujo(flujo, subjectId);

        //        if (respuestaComplementaria.EsExito)
        //        {
        //            Repositorio.Add(respuestaComplementaria.Contenido);

        //            var save = Repositorio.Try(r => r.Save());

        //            if (save.EsError)
        //            {
        //                return save.ErrorBaseDatos(TAG);
        //            }

        //            return new Respuesta();
        //        }

        //        return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
        //    }

        //    return new Respuesta(respuesta.Mensaje, TAG);

        //}


        //public Respuesta Modificar(List<TFlujo> flujo, string subjectId)
        //{

        //    if (flujo.Count() <= 0)
        //        return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

        //    var esPredeterminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo[0].IdTipoEnte));

        //    if (esPredeterminado.EsError)
        //        return esPredeterminado.ErrorBaseDatos(TAG);


        //    var flujoOriginal = Repositorio.Try(r => r.ObtenerFlujosPorEntePublico(flujo[0].IdTipoEnte));

        //    if (flujoOriginal.EsError)
        //        return flujoOriginal.ErrorBaseDatos(TAG);

        //    //flujo.ForEach(flujo => {
        //    //    flujoOriginal.Contenido.Add(flujo);
        //    //});


        //    var respuesta = ServicioDominio.Modificar(flujo, flujoOriginal.Contenido, esPredeterminado.Contenido, false, subjectId);

        //    if (respuesta.EsExito)
        //    {

        //        var respuestaComplementaria = this.ModificarFlujo(flujo, respuesta.Contenido, subjectId);

        //        if (respuestaComplementaria.EsExito)
        //        {
        //            var save = Repositorio.Try(r => r.Save());
        //            if (save.EsError)
        //                return save.ErrorBaseDatos(TAG);

        //            return new Respuesta();
        //        }

        //        return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
        //    }

        //    return new Respuesta(respuesta.Mensaje, respuesta.TAG);

        //}

        ///// <summary>
        ///// Elimina los flujos de manera individual que tenga un Ente Publico 
        ///// </summary>
        ///// <param name="flujo"></param>
        ///// <param name="subjectId"></param>
        ///// <returns></returns>
        //public Respuesta Eliminar(List<int> IdsFlujos, string subjectId)
        //{
        //    if (IdsFlujos.Count() <= 0)
        //        return new Respuesta("Es requerido un flujo de autorizacion", TAG);

        //    List<TFlujo> flujosOriginales = null;

        //    foreach (var idFlujo in IdsFlujos)
        //    {
        //        var flujo = Repositorio.Try(r => r.Get(x => x.Id == idFlujo));

        //        if (flujo.EsError)
        //            return flujo.ErrorBaseDatos();

        //        var respuesta = ServicioDominio.Eliminar(flujo.Contenido, subjectId);

        //        if (respuesta.EsError)
        //            return new Respuesta(respuesta.Mensaje, TAG);

        //        flujosOriginales.Add(respuesta.Contenido);
        //    }

        //    var respuestaComplementaria = this.EliminarFlujo(flujosOriginales, subjectId);

        //    if (respuestaComplementaria.EsExito)
        //    {

        //        var guardar = Repositorio.Try(r => r.Save());
        //        if (guardar.EsError)
        //            return guardar.ErrorBaseDatos(TAG);

        //        return new Respuesta();
        //    }

        //    return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
        //}
    }
}
