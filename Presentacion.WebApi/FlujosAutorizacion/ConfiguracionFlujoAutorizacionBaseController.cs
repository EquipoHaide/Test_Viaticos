using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Repositorios;
using Dominio.Viaticos.Modelos;
using EntidadesViaticos = Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;
using System;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.FlujosAutorizacion
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
                var consulta = ServicioConfiguracionFlujoBase.Consultar(filtro, this.GetSubjectId());

                if (consulta.EsError)
                {
                    if (consulta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(consulta.Mensaje);
                }

                return this.ApiResult(new { consulta });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }


        [HttpPost("recursos")]
        
        public object Crear([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config)   
        {
            try {
               
                var resultado = ServicioConfiguracionFlujoBase.Crear(config.Flujo, this.GetSubjectId());

                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado});
            }
             catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }

        }


        [HttpPut("recursos")]
        public object Modificar([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config)
        {
            try
            {
                var resultado = ServicioConfiguracionFlujoBase.Modificar(config.Flujo, this.GetSubjectId());
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


        [HttpDelete("recursos")]
        public object Eliminar([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config)
        {
            throw new NotImplementedException();
        }

    }
}