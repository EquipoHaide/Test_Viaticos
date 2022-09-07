﻿using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.RecursosProtegidos
{
    public class ConfiguracionFlujoAutorizacionBaseController<TFlujo, TConsulta> : ControllerBase, IConfiguracionFlujoAutorizacionBaseController<TFlujo, TConsulta>
         where TConsulta : IConsulta
         //where TFlujo : IFlujoNew
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
            

            //List<Dominio.Nucleo.IFlujoNew> f = new List<IFlujoNew>();

            //flujos.ForEach(s => { f.Add((IFlujoNew)s); });
                       

            ((IServicioFlujosNew<TFlujo>)ServicioConfiguracionFlujo).CrearViaticos(flujos);

       
            Console.WriteLine("TEST");
            return null;

        }
    }
}