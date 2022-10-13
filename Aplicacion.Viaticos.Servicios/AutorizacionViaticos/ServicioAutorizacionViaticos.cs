using System;
using Aplicacion.Nucleo.ServicioAutorizacion;
using Entidades = Dominio.Viaticos.Entidades;
using Modelos = Dominio.Viaticos.Modelos;
using DominioServicio = Dominio.Viaticos.Servicios;
using ServicioBase = Dominio.Nucleo.Servicios.ServicioAutorizacion;
using Dominio.Viaticos.Repositorios;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;

namespace Aplicacion.Viaticos.Servicios.AutorizacionViaticos
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion, Modelos.ConsultaSolicitudes>,
        IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion, Modelos.ConsultaSolicitudes>
    {

        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }

        DominioServicio.IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion> servicio;
        DominioServicio.IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion> Servicio => App.Inject(ref servicio);
        public override ServicioBase.IServicioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion> ServicioDominio => this.Servicio;

        IRepositorioAutorizacionViaticos repositorioAutorizacion;
        IRepositorioAutorizacionViaticos RepositorioAutorizacion => App.Inject(ref repositorioAutorizacion);
        public override IRepositorioAutorizacionBase<Entidades.SolicitudCondensada, Entidades.Autorizacion,Modelos.ConsultaSolicitudes> Repositorio => this.RepositorioAutorizacion;


        public ServicioAutorizacionViaticos(Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
