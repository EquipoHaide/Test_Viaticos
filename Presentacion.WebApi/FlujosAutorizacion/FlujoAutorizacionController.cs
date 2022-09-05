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
    public class FlujoAutorizacionController<TFlujo, TConsulta> : ControllerBase, IFlujoAutorizacionController<TFlujo, TConsulta>
         where TConsulta : IConsulta
         where TFlujo : IFlujoNew
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
        public virtual IServicioRecursoBase ServicioRecursos { get; }
        public virtual IServicioConfiguracionFlujoBaseNew ServicioConfiguracionFlujo { get; }

        [HttpGet("recursos")]
        public object ConsultarConfiguracionFlujo([FromQuery] TConsulta filtro) 
        {
           
            ServicioConfiguracionFlujo.Consultar(filtro, "");

            Console.WriteLine("TEST");
            return null;
        }

        [HttpPost("recursos")]
        public object Crear([FromBody] List<TFlujo> flujos)   
        {
            //ServicioConfiguracionFlujo.Crear(flujos, "");

            //·······  >>  PENDIENTE PARA EL LUNES PREGUNTAR A ROBERTO COMO SE HACE PARA QUE EN EL CONTROLADOR 
            //              SE PUEDA RESOLVER UNA INTERFACE, QUE ESTA COMO PROPIEDAD DE OTRA INTERFACE
            Console.WriteLine("TEST");
            return null;

        }
    }
}