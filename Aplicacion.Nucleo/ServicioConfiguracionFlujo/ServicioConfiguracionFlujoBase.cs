﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

using Infraestructura.Datos.Nucleo;
using Entidades = Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;

using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TFlujo, TPaso, TQuery> : IServicioConfiguracionFlujoBase<TFlujo, TPaso, TQuery>
        where TFlujo : class,IFlujo<TPaso>
        where TPaso : class,IPaso
        where TQuery : class, IConsultaFlujo
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TFlujo,TPaso> ServicioDominio { get; }


        public virtual IRepositorioConfiguracionFlujo<TFlujo, TPaso, TQuery> Repositorio { get; }
        

        /// <summary>
        /// PENDIENTE :
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        //public abstract Respuesta<bool> ValidarPasos(IFlujo<TPaso> flujos);
        public abstract Respuesta<TFlujo> CreacionFlujo(TFlujo flujos, string subjectId);

        public abstract Respuesta<TFlujo> ModificarFlujo(TFlujo flujos, string subjectId);

        public abstract Respuesta<TFlujo> EliminarFlujo(TFlujo flujos, string subjectId);


        public Respuesta<ConsultaPaginada<List<TFlujo>>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<List<TFlujo>>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarFlujosDeAutorizacion(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<List<TFlujo>>>(TAG);

            return new Respuesta<ConsultaPaginada<List<TFlujo>>>("");

        }

        public Respuesta Crear(TFlujo flujo, string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            //CONSULTA AL REPOSITORIO
            //BUSCAR SI YA EXISTE UN FLUJO PREDETERMINADO ANTES QUE UN FLUJO PARTICULAR 
            //            
            var esPredertiminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo.TipoEntePublico?.Id == null ? 0 : flujo.TipoEntePublico.Id));

            if (esPredertiminado.EsError)
                return esPredertiminado.ErrorBaseDatos(TAG);


            //CONSULTA AL REPOSITORIO 
            //BUSCA SI EXITE ALGUN FLUJO PARTICULAR CON EL MISMO NIVEL 
            //
            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido(flujo.TipoEntePublico?.Id == null ? 0 : flujo.TipoEntePublico.Id, flujo.NivelEmpleado.Nivel == null ? "" : flujo.NivelEmpleado.Nivel));


            if (esNivelRepetido.EsError)
                return esPredertiminado.ErrorBaseDatos(TAG);


            var respuesta = ServicioDominio.Crear(flujo, esPredertiminado.Contenido, esNivelRepetido.Contenido, subjectId);


            if (respuesta.EsExito)
            {
                var respuestaComplementaria= this.CreacionFlujo(flujo,subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    Repositorio.Add(respuestaComplementaria.Contenido);

                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                    {
                        return save.ErrorBaseDatos(TAG);
                    }

                    return new Respuesta();
                }

                return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }

            return new Respuesta(respuesta.Mensaje, TAG);

        }


        public Respuesta Modificar(TFlujo flujo,  string subjectId)
        {

            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            //CONSULTA AL REPOSITORIO
            //BUSCAR SI YA EXISTE UN FLUJO PREDETERMINADO ANTES QUE UN FLUJO PARTICULAR 
            //            
            var esPredertiminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo.TipoEntePublico?.Id == null ? 0 : flujo.TipoEntePublico.Id));

            if (esPredertiminado.EsError)
                return esPredertiminado.ErrorBaseDatos(TAG);


            //CONSULTA AL REPOSITORIO 
            //BUSCA SI EXITE ALGUN FLUJO PARTICULAR CON EL MISMO NIVEL 
            //
            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido(flujo.TipoEntePublico?.Id == null ? 0 : flujo.TipoEntePublico.Id, flujo.NivelEmpleado.Nivel == null ? "" : flujo.NivelEmpleado.Nivel));

            if (esNivelRepetido.EsError)
                return esPredertiminado.ErrorBaseDatos(TAG);


            var flujoOriginal = Repositorio.Try(r => r.Get(g => g.Id == flujo.Id));

            if (flujoOriginal.EsError)
                return flujoOriginal.ErrorBaseDatos(TAG);

         
            var respuesta = ServicioDominio.Modificar(flujo, flujoOriginal.Contenido, esPredertiminado.Contenido,esNivelRepetido.Contenido, subjectId);


            if (respuesta.EsExito)
            {
                var respuestaComplementaria = this.ModificarFlujo(flujo, subjectId);

                if (respuestaComplementaria.EsExito)
                {

                    var save = Repositorio.Try(r => r.Save());
                    if (save.EsError) return save.ErrorBaseDatos(TAG);

                    return new Respuesta();
                }
                
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }

            return new Respuesta(respuesta.Mensaje, respuesta.TAG);

        }

        public Respuesta Eliminar(TFlujo flujo,string subjectId)
        {
            
           var flujosOriginales = Repositorio.Try(r => r.ObtenerFlujos(flujo, subjectId));
 
            if (flujosOriginales.EsError)
                return flujosOriginales.ErrorBaseDatos();

            var respuesta = ServicioDominio.Eliminar(flujosOriginales.Contenido, subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, TAG);


            var respuestaComplementaria = this.EliminarFlujo(flujo,subjectId);

            if (respuestaComplementaria.EsExito)
            {
                Repositorio.Remove(respuesta.Contenido);

                var guardar = Repositorio.Try(r => r.Save());
                if (guardar.EsError)
                    return guardar.ErrorBaseDatos(TAG);

                return new Respuesta();
            }

            return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
        }




    }
}
