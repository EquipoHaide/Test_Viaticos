using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public class ConfiguracionFlujoAutorizacionBaseController<TPaso, TConsulta> : ControllerBase, IConfiguracionFlujoAutorizacionBaseController<TPaso, TConsulta>
         where TConsulta : IConsulta
         where TPaso : IPaso
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
        //public virtual IServicioRecursoBase ServicioRecursos { get; }
        public virtual IServicioConfiguracionFlujoBaseNew ServicioConfiguracionFlujo { get; }
        public virtual Aplicacion.Nucleo.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioConfiguracionFlujoBase { get; }


        [HttpGet("recursos")]
        public object ConsultarConfiguracionFlujo([FromQuery] TConsulta filtro) 
        {
           
            var respuesta = ServicioConfiguracionFlujo.Consultar(filtro, "");

            Console.WriteLine("TEST");
            return null;
        }

        [HttpPost("recursos")]
        public object Crear([FromBody] ModeloConfiguracionFlujo<TPaso> config)   
        {
            try {
                var flujos = new List<IFlujo<TPaso>>();
                config.Flujos.ForEach(f => { flujos.Add(f); });

                var flujosConvertidos = config.Flujos as List<IFlujo<PasoViatico>>;
                var respuesta = ServicioConfiguracionFlujoBase.Crear(flujos/*config.Flujos as List<IFlujo<TPaso>>*/);

            } catch (Exception ex) {

            }

            Console.WriteLine("TEST");
            return null;

        }
    }
}