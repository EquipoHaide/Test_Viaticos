using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.AutorizacionSolicitudes
{
    public class AutorizacionSolicitudBaseController<TInstanciaCondensada,TAutorizacion,TFlujo,TPaso, TQuery> : ControllerBase, IAutorizacionSolicitudBaseController<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso, TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada :class, ISolicitudCondensada
        where TQuery : class,IQuery
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }

        public virtual Aplicacion.Nucleo.ServicioAutorizacion.IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion,TFlujo,TPaso,TQuery> ServicioAutorizacion { get; }


        [HttpGet("recursos")]
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

                return this.ApiResult(consulta.Contenido);
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpPut("recursos")]
       
        public object AdministrarAutorizaciones([FromBody] ModeloSolicitud<TInstanciaCondensada,TFlujo,TPaso> modelo)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())

                var resultado = ServicioAutorizacion.AdministrarAutorizaciones(modelo.Solicitudes, modelo.Flujos, modelo.Accion, GetSubjectId);

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
