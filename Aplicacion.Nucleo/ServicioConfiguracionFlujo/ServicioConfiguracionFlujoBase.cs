using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Entidades = Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;

using Infraestructura.Transversal.Plataforma.Extensiones;
using System;
using System.Collections.Generic;

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
        

        /// <summary>
        /// El CrearFlujo se usa para añadir las validaciones adicionales de tu flujo 
        /// </summary>
        public abstract Respuesta<TFlujo> CreacionFlujo(TFlujo flujos, string subjectId);

        /// <summary>
        /// El ModificarFlujo se usa para añadir las validaciones adicionales de tu flujo 
        /// </summary>
        public abstract Respuesta<TFlujo> ModificarFlujo(TFlujo flujos,TFlujo flujoOriginal, string subjectId);

        /// <summary>
        /// El EliminarFlujo se usa para añadir las validaciones adicionales requeridas para tu flujo 
        /// </summary>
        public abstract Respuesta<TFlujo> EliminarFlujo(TFlujo flujos, string subjectId);


        public Respuesta<ConsultaPaginada<TFlujo>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<TFlujo>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarFlujosDeAutorizacion(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<TFlujo>>(TAG);

            return new Respuesta<ConsultaPaginada<TFlujo>>(recursos.Contenido);
        }

        public Respuesta Crear(TFlujo flujo, string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            var esPredeterminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo));

            if (esPredeterminado.EsError)
                return esPredeterminado.ErrorBaseDatos(TAG);

            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido(flujo));

            if (esNivelRepetido.EsError)
                return esPredeterminado.ErrorBaseDatos(TAG);

            var esEntePublico = Repositorio.Try(r => r.ExisteRegistroEntePublico(flujo));

            var respuesta = ServicioDominio.Crear(flujo, esPredeterminado.Contenido, esNivelRepetido.Contenido, esEntePublico.Contenido, subjectId);

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
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);
        
            var esPredeterminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo));

            if (esPredeterminado.EsError)
                return esPredeterminado.ErrorBaseDatos(TAG);

            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido(flujo));

            if (esNivelRepetido.EsError)
                return esPredeterminado.ErrorBaseDatos(TAG);

            var flujoOriginal = Repositorio.Try(r => r.ObtenerFlujo(flujo.Id));
         
            if (flujoOriginal.EsError)
                return flujoOriginal.ErrorBaseDatos(TAG);

            var respuesta = ServicioDominio.Modificar(flujo, flujoOriginal.Contenido, esPredeterminado.Contenido,esNivelRepetido.Contenido, subjectId);

            if (respuesta.EsExito)
            {
               
                var respuestaComplementaria = this.ModificarFlujo(flujo,respuesta.Contenido, subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    var save = Repositorio.Try(r => r.Save());
                    if (save.EsError)
                        return save.ErrorBaseDatos(TAG);

                    return new Respuesta();
                }

                return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }

            return new Respuesta(respuesta.Mensaje, respuesta.TAG);

        }

        /// <summary>
        /// Elimina los flujos de manera individual que tenga un Ente Publico 
        /// </summary>
        /// <param name="flujo"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public Respuesta Eliminar(int Id,string subjectId)
        {
            
            var flujoOriginale = Repositorio.Try(r => r.ObtenerFlujo(Id));

            if (flujoOriginale.EsError)
                return flujoOriginale.ErrorBaseDatos();

            var respuesta = ServicioDominio.Eliminar(flujoOriginale.Contenido,subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, TAG);

            var respuestaComplementaria = this.EliminarFlujo(respuesta.Contenido,subjectId);

            if (respuestaComplementaria.EsExito)
            {
             
                var guardar = Repositorio.Try(r => r.Save());
                if (guardar.EsError)
                    return guardar.ErrorBaseDatos(TAG);

                return new Respuesta();
            }

            return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
        }




    }
}
