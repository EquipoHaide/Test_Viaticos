using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.AutorizacionSolicitudes
{
    public class AutorizacionSolicitudBaseController<TInstanciaCondensada,TAutorizacion, TQuery> : ControllerBase, IAutorizacionSolicitudBaseController<TInstanciaCondensada, TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada :class, IInstanciaCondensada
        where TQuery : class,IConsultaSolicitud
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }

        public virtual Aplicacion.Nucleo.ServicioAutorizacion.IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion,TQuery> ServicioAutorizacion { get; }


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
       
        public object AdministrarAutorizaciones([FromBody] ModeloSolicitud<TInstanciaCondensada> modelo)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())

                var resultado = ServicioAutorizacion.AdministrarAutorizaciones(modelo.Solicitudes, modelo.Accion, GetSubjectId);

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
