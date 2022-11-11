using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;
using System;
using Presentacion.WebApi.Modelos;
using System.Collections.Generic;

namespace Presentacion.WebApi.ConfiguracionFlujo
{
    public class ConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso, TQuery> : ControllerBase, IConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso, TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class , IConsultaFlujo
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
 
        public virtual Aplicacion.Nucleo.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TFlujo,TPaso,TQuery> ServicioConfiguracionFlujoBase { get; }


        [HttpGet("recursos")]
        public object ConsultarConfiguracionFlujo([FromQuery] TQuery filtro)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var consulta = ServicioConfiguracionFlujoBase.Consultar(filtro, GetSubjectId);

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


        [HttpPut("administrar")]
        public object AdministrarFlujos([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var resultado = ServicioConfiguracionFlujoBase.AdministrarFlujos(config.Flujos, GetSubjectId);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado.Contenido });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }

        [HttpGet("flujo")]
        public object ObtenrConfiguracionFlujo([FromQuery] int idFlujo)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var resultado = ServicioConfiguracionFlujoBase.ObtenerConfiguracionFlujo(idFlujo);

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado.Contenido });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }
    }
}