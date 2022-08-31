﻿using Aplicacion.Nucleo;
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

        public ConfiguracionFlujoController(IAplicacion app)
        {
            this.App = app;
        }


        [HttpGet()]
        public Object Consultar(ConsultaConfiguracionFlujo filtro)
        {

            Console.WriteLine("TEST---");





            return null;
        }


     
    }
}