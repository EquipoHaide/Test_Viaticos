﻿using System;
using System.Collections.Generic;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Transversal.Plataforma;
using DominioServicio = Dominio.Viaticos.Servicios;
using EntidadesViaticos = Dominio.Viaticos.Entidades;
namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioFlujo : ServicioConfiguracionFlujoBase<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico>, IServicioFlujo<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }


        DominioServicio.IServicioFlujos<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> servicio;
        DominioServicio.IServicioFlujos<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> Servicio => App.Inject(ref servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);

        public override IRepositorioConfiguracionFlujo<Dominio.Viaticos.Entidades.FlujoViaticos, PasoViatico> Repositorio => this.RepositorioConfiguracionFlujoViaticos;





        public ServicioFlujo(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<bool> ValidarPasos(IFlujo<PasoViatico> flujos)
        {
         
            //Agregar las validaciones pertienentes a los pasos para los flujos de viaticos

            return new Respuesta<bool>(false); 
        }

       
    }
}
