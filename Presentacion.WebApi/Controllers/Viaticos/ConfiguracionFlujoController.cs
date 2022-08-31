using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Seguridad.Modelos;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.RecursosProtegidos;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ConfiguracionFlujoController : ControllerBase
    {

        public Aplicacion.Nucleo.IAplicacion App { get; set; }


        IServicioFlujos servicio;
        IServicioFlujos Servicio => App.Inject(ref servicio);

        public ConfiguracionFlujoController(IAplicacion app)
        {
            this.App = app;
        }


        [HttpGet()]
        public Object Consultar(ConsultaConfiguracionFlujo filtro)
        {

           var respuesta =  Servicio.Consultar(filtro, "");
            
            Console.WriteLine("TEST---");





            return null;
        }




        [HttpGet("{id:int}")]
        public Object Obtener(int id)
        {

            Console.WriteLine("TEST---");





            return null;
        }



    }
}