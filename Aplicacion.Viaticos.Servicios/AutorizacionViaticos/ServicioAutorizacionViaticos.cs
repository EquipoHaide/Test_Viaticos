using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Entidades = Dominio.Viaticos.Entidades;
using Modelos = Dominio.Viaticos.Modelos;
using DominioServicio = Dominio.Viaticos.Servicios;
using ServicioBase = Dominio.Nucleo.Servicios.ServicioAutorizacion;
using Dominio.Viaticos.Repositorios;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using AplicacionViaticos = Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;
using Dominio.Viaticos.Modelos;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;
using Dominio.Viaticos.Entidades;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion,
                                                                        Entidades.FlujoViatico, Entidades.PasoViatico, Modelos.ConsultaSolicitudes>,
        IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion, Entidades.FlujoViatico, Entidades.PasoViatico, Modelos.ConsultaSolicitudes>
    {

        const string TAG = "Aplicacion.Viaticos.Servicios.AutorizacionViaticos";

        Nucleo.IAplicacion App { get; set; }

        DominioServicio.IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion,Entidades.FlujoViatico, Entidades.PasoViatico> servicio;
        DominioServicio.IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion,Entidades.FlujoViatico, Entidades.PasoViatico> Servicio => App.Inject(ref servicio);
        public override ServicioBase.IServicioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion, Entidades.FlujoViatico, Entidades.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioAutorizacionViaticos repositorioAutorizacion;
        IRepositorioAutorizacionViaticos RepositorioAutorizacion => App.Inject(ref repositorioAutorizacion);
        public override IRepositorioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion,Modelos.ConsultaSolicitudes> Repositorio => this.RepositorioAutorizacion;


        public ServicioAutorizacionViaticos(Nucleo.IAplicacion app): base(app)
        {
            this.App = app;
        }

        public override Respuesta AdministracionFinalAutorizacion(List<SolicitudCondensada> solicitudes, List<Autorizacion> autorizaciones, string subjectId)
        {

          

            return new Respuesta();
        }
    }
}
