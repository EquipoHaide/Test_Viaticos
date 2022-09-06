
using Aplicacion.Viaticos.Servicios;
using Dominio.Viaticos.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using Nancy;
using System.Text.Json;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ConfiguracionFlujoController : ControllerBase
    {

        public Aplicacion.Nucleo.IAplicacion App { get; set; }


        IServicioFlujos servicio;
        IServicioFlujos Servicio => App.Inject(ref servicio);
     



        public ConfiguracionFlujoController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }


        [HttpGet()]
        public Object Consultar(ConsultaConfiguracionFlujo filtro)
        {

            var respuesta = Servicio.Consultar(filtro,"");
            
            Console.WriteLine("TEST---");





            return null;
        }

        //[HttpPost(Name = "CrearFlujos")]
        public object Crear([FromBody]List<FlujoViaticos> flujos)
        {

            var respuesta = 0;
            //List<IFlujo> listaFlujos = new List<IFlujo>();
            //flujos.ForEach(f => {
            //    listaFlujos.Add(f);
            //});

            // var respuesta = Servicio.Crear(flujos, "");
            return null;
        }

        [HttpPost()]
        public object Create([FromBody] List<Flujo> flujo)
        {
            try {

                var lista = new List<Flujo>();
                //lista.Add(flujo);
                var respuesta = Servicio.Crear(flujo, "");

            }
            catch(Exception e) {

            }
           
            //var respuesta = JsonSerializer.Deserialize(flujo, d);
            //WeatherForecast? weatherForecast =
            // JsonSerializer.Deserialize<WeatherForecast>(jsonString);
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