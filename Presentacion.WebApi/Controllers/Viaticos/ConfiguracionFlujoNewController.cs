using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Nucleo;
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
    public class ConfiguracionFlujoNewController : FlujoAutorizacionController< FlujoViaticos, ConsultaConfiguracionFlujo>
    {

        IServicioFlujosNew servicio;
        IServicioFlujosNew Servicio => App.Inject(ref servicio);


        public override IServicioConfiguracionFlujoBaseNew ServicioConfiguracionFlujo => Servicio;

        public ConfiguracionFlujoNewController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }


     
    }
}