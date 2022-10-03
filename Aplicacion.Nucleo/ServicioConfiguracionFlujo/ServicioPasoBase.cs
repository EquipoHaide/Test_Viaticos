using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioPasoBase<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioPasoBase<TPaso> ServicioDominio { get; }

        public virtual IRepositorioPasoBase<TPaso> Repositorio { get; }

        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";
        // <summary>
        /// El ModificarPaso se usa para añadir las validaciones adicionales de tu paso  
        /// </summary>
        public abstract Respuesta<TPaso> ModificarPaso(TPaso paso,TPaso pasoOriginal, string subjectId);

        // <summary>
        /// El ModificarPaso se usa para añadir las validaciones adicionales de tu paso  
        /// </summary>
        public abstract Respuesta<TPaso> EliminarPaso(TPaso paso, string subjectId);

        public abstract Respuesta ReordenarPosicionesPorEliminacion(List<TPaso> listaPasos,string subjectId);

        public Respuesta<List<TPaso>> Consultar(int idFlujo)
        {
        
            var listaPasos = Repositorio.Try(r => r.ObtenerPasos(idFlujo <= 0 ? 0 : idFlujo));

            if (listaPasos.EsError)
                return new Respuesta<List<TPaso>>("Error de Base de Datos",TAG);

            return new Respuesta<List<TPaso>>(listaPasos.Contenido);
        }

        public Respuesta Eliminar(int id, string subjectId)
        {

            var pasoOriginal = Repositorio.Try(r => r.Get(x => x.Id == id));

            if (pasoOriginal.EsError)
                return pasoOriginal.ErrorBaseDatos(TAG);

            var listaPasos = Repositorio.Try(r => r.ObtenerPasos(pasoOriginal.Contenido?.IdConfiguracionFlujo == null ? 0 : pasoOriginal.Contenido.IdConfiguracionFlujo));

            var respuesta = ServicioDominio.Eliminar(pasoOriginal.Contenido, subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);

            if (respuesta.EsExito)
            {
                var respuestaComplementaria = this.EliminarPaso(pasoOriginal.Contenido, subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    var camposOrdenar = Repositorio.Try(r => r.ObtenerPasosReordenar(pasoOriginal.Contenido.Id, pasoOriginal.Contenido.IdConfiguracionFlujo));

                    if (camposOrdenar.EsError)
                        return camposOrdenar.ErrorBaseDatos(camposOrdenar.TAG);

                    var reordernacion = ServicioDominio.ReordenarPosicionesPorEliminacion(camposOrdenar.Contenido, subjectId);

                    if (reordernacion.EsError)
                        return reordernacion.Error();

                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                        return save.ErrorBaseDatos(TAG);

                    return new Respuesta();
                }

                return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }


            return new Respuesta();
        }

        public Respuesta Modificar(TPaso paso, string subjectId)
        {

            var pasoOriginal = Repositorio.Try(r => r.Get(x => x.Id == paso.Id));

            if (pasoOriginal.EsError)
                return pasoOriginal.ErrorBaseDatos(TAG);

            var listaPasos = Repositorio.Try(r => r.ObtenerPasos(paso.IdConfiguracionFlujo));

            var respuesta = ServicioDominio.Modificar(paso, pasoOriginal.Contenido, subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje,respuesta.TAG);

            if (respuesta.EsExito)
            {
                var respuestaComplementaria = this.ModificarPaso(paso, pasoOriginal.Contenido, subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                        return save.ErrorBaseDatos(TAG);
                    
                    return new Respuesta();
                }

                return new Respuesta(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }

            return new Respuesta();
        }
    }
}
