using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.RecursosProtegidos
{
    public class RecursoProtegidoController<TPermiso, TConsulta> : ControllerBase, IRecursoProtegidoController<TPermiso, TConsulta>
        where TPermiso : PermisoBase, IPermisoModel
        where TConsulta : IModeloConsultaRecurso
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
        public virtual IServicioRecursoBase ServicioRecursos { get; }

        [HttpGet("recursos")]
        public object ConsultarRecursos([FromQuery] TConsulta filtro)
        {
            try
            {
                var consulta = ServicioRecursos.ConsultarRecursos(filtro, this.GetSubjectId());

                if (consulta.EsError)
                {
                    if (consulta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(consulta.Mensaje);
                }

                var permisos = new List<TPermiso>();
                consulta.Contenido.Elementos.ToList().ForEach(e => {
                    permisos.Add(e as TPermiso);
                });

                var consultaGenerica = consulta.Contenido;

                ConsultaPaginada<TPermiso> consultaTipada = new ConsultaPaginada<TPermiso>(consultaGenerica.Pagina.Value, consultaGenerica.ElementosPorPagina.Value, consultaGenerica.TotalElementos, permisos);

                return this.ApiResult(consultaTipada);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPost("recursos")]
        public object AdministrarRecursos([FromBody] List<TPermiso> permisos)
        {
            try
            {
                var respuesta = ServicioRecursos.AdministrarRecursos(permisos, this.GetSubjectId());

                if (respuesta.EsError)
                {
                    if (respuesta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(respuesta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(respuesta.Mensaje);
                }

                return this.ApiResult(respuesta.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }
    }
}