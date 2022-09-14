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

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public class ConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso> : ControllerBase, IConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
 
        public virtual Aplicacion.Nucleo.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TFlujo,TPaso> ServicioConfiguracionFlujoBase { get; }

        //IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        //IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);

        //public virtual IRepositorioConfiguracionFlujo<TFlujo,TPaso> RepositorioViaticos => this.RepositorioConfiguracionFlujoViaticos;


        //[HttpGet("recursos")]
        //public object ConsultarConfiguracionFlujo([FromQuery] TConsulta filtro)
        //{
        //    try
        //    {
        //        var consulta = ServicioConfiguracionFlujoBase.Consultar(filtro, this.GetSubjectId());

        //        if (consulta.EsError)
        //        {
        //            if (consulta.Estado == EstadoProceso.Fatal)
        //                return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

        //            return this.ApiResult(consulta.Mensaje);
        //        }

        //        return this.ApiResult(new { consulta });
        //    }
        //    catch (Exception e)
        //    {
        //        return this.ApiResult(e, App.GetLogger());
        //    }
        //}


        [HttpPost("recursos")]
        
        public object Crear([FromBody] TFlujo config)   
        {
            try {
               
                var resultado = ServicioConfiguracionFlujoBase.Crear(config, this.GetSubjectId());

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
        public object Modificar([FromBody] TFlujo config)
        {
            try
            {
                var resultado = ServicioConfiguracionFlujoBase.Modificar(config, this.GetSubjectId());
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
        public object Eliminar(TFlujo config)
        {
            throw new NotImplementedException();
        }

    }
}