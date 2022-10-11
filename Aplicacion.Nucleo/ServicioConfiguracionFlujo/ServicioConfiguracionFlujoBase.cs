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
            if (flujos == null || flujos.Count() <= 0)
                return new Respuesta<List<TFlujo>>("Es requerido un flujo de autorizacion", TAG);           

            var flujosExistentes = Repositorio.Try(r => r.ObtenerFlujosPorEntePublico(flujos[0].IdTipoEnte));

            if (flujosExistentes.EsError)
                return new Respuesta<List<TFlujo>>(flujosExistentes.Mensaje, TAG);

            var existeEntePublico = Repositorio.Try(r => r.ExisteRegistroEntePublico(flujos[0]));

            if (existeEntePublico.EsError)
                return new Respuesta<List<TFlujo>>(existeEntePublico.Mensaje, TAG);

            var respuesta = ServicioDominio.AdministrarFlujos(flujos, flujosExistentes.Contenido, existeEntePublico.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                var respuestaComplementaria = this.AdministrarConfiguracionFlujos(respuesta.Contenido, subjectId);

                if (respuestaComplementaria.EsExito)
                {
                    foreach (var item in respuesta.Contenido)
                    {
                        if (item.Id == 0)
                            Repositorio.Add(item); 
                    }
                    
                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                    {
                        return new Respuesta<List<TFlujo>>(save.Mensaje, save.TAG);
                    }

                    return new Respuesta<List<TFlujo>>(save.Mensaje);
                }

                return new Respuesta<List<TFlujo>>(respuestaComplementaria.Mensaje, respuestaComplementaria.TAG);
            }
            
            return new Respuesta<List<TFlujo>>(respuesta.Mensaje,TAG);
        }
    }
}
