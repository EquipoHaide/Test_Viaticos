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
using Dominio.Viaticos.Repositorios;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    [Route("api/[controller]")]
    //[Authorize]
    public class ConfiguracionFlujoViaticosController : ConfiguracionFlujoAutorizacionBaseController<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico>
    {

        AplicacionViaticos.IServicioFlujo<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> servicioViaticos;
        AplicacionViaticos.IServicioFlujo<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> ServicioViaticos => App.Inject(ref servicioViaticos);

        //public override IServicioConfiguracionFlujoBaseNew ServicioConfiguracionFlujo => Servicio;

        //IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        //IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);


        public override IServicioConfiguracionFlujoBase<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> ServicioConfiguracionFlujoBase => this.ServicioViaticos;


        public ConfiguracionFlujoViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;

            //ServicioConfiguracionFlujoBase.Crear(null);
        }

        
    }
}