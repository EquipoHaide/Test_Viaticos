using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Nucleo;
using Dominio.Seguridad.Modelos;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.FlujosAutorizacion;
using Presentacion.WebApi.Modelos;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ConfiguracionFlujoViaticosController : ConfiguracionFlujoAutorizacionBaseController<ModeloPaso, ConsultaConfiguracionFlujo>
    {

        IServicioFlujos<ModeloPaso> servicio;
        IServicioFlujos<ModeloPaso> Servicio => App.Inject(ref servicio);

        AplicacionViaticos.IServicioFlujo<ModeloPaso> servicioViaticos;
        AplicacionViaticos.IServicioFlujo<ModeloPaso> ServicioViaticos => App.Inject(ref servicioViaticos);

        //public override IServicioConfiguracionFlujoBaseNew ServicioConfiguracionFlujo => Servicio;

        public override IServicioConfiguracionFlujoBase<ModeloPaso> ServicioConfiguracionFlujoBase => this.Servicio;


        public ConfiguracionFlujoViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;

            //ServicioConfiguracionFlujoBase.Crear(null);
        }

        
    }
}