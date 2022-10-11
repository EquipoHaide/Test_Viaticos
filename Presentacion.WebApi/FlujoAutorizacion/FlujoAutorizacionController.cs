using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi.FlujoAutorizacion
{
    public class FlujoAutorizacionController<TAutorizacion, TQuery> : ControllerBase, IFlujoAutorizacionController<TAutorizacion, TQuery>
        where TAutorizacion :class, IAutorizacion
        where TQuery : class,IConsultaSolicitud
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }

        public virtual Aplicacion.Nucleo.ServicioAutorizacion.IServicioAutorizacionBase<TAutorizacion,TQuery> ServicioAutorizacion { get; }


        [HttpGet("solicitudes")]
        public object ConsultarSolicitudes([FromQuery] TQuery filtro)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var consulta = ServicioAutorizacion.Consultar(filtro, GetSubjectId);

                if (consulta.EsError)
                {
                    if (consulta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(consulta.Mensaje);
                }

                return this.ApiResult(/*consulta.Contenido*/);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPut("solicitudes")]
        public object AdministrarAutorizaciones([FromBody] List<TAutorizacion> autorizacion)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())

                var resultado = ServicioAutorizacion.AdministrarAutorizaciones(autorizacion  , GetSubjectId);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado });
             
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }
    }
}
