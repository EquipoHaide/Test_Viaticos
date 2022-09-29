using System;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.ConfiguracionFlujo
{
    public class PasosBaseController<TPaso> : ControllerBase, IPasosBaseController<TPaso>
        where TPaso : class, IPaso
    {

        public Aplicacion.Nucleo.IAplicacion App { get; set; }

        public virtual Aplicacion.Nucleo.ServicioConfiguracionFlujo.IServicioPasoBase<TPaso> ServicioPasosBase  { get;}

        [HttpDelete()]
        public object Eliminar([FromQuery] int id)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var resultado = ServicioPasosBase.Eliminar(id, GetSubjectId);
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

        [HttpPut()]
        public object Modificar([FromBody] ModeloPaso<TPaso> config)
        {
            try
            {
                var GetSubjectId = "asdgasdghjas"; // this.GetSubjectId())
                var resultado = ServicioPasosBase.Modificar(config.Paso, GetSubjectId);
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
