using System;
using Aplicacion.Nucleo.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Entidades = Dominio.Viaticos.Entidades;
using DominioServicio = Dominio.Viaticos.Servicios;
using Dominio.Viaticos.Repositorios;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;

namespace Aplicacion.Viaticos.Servicios.ConfiguracionFlujos
{
    public class ServicioPaso : ServicioPasoBase<Entidades.PasoViatico>, IServicioPaso<Entidades.PasoViatico>
    {
        const string TAG = "Aplicacion.Viaticos.Servicios.ConfiguracionFlujos";

        Nucleo.IAplicacion App { get; set; }
        DominioServicio.IServicioPasos<Entidades.PasoViatico> servicio;
        DominioServicio.IServicioPasos<Entidades.PasoViatico> Servicio => App.Inject(ref this.servicio);
        public override Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioPasoBase<Entidades.PasoViatico> ServicioDominio => this.Servicio;

        IRepositorioPaso repositorioPaso;
        IRepositorioPaso RepositorioPaso => App.Inject (ref this.repositorioPaso);
        public override IRepositorioPasoBase<Entidades.PasoViatico> Repositorio => this.RepositorioPaso;

        public ServicioPaso(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<PasoViatico> EliminarPaso(PasoViatico paso, string subjectId)
        {
            var respuesta = Servicio.Eliminar(paso,false, subjectId);

            if(respuesta.EsError)
                return new Respuesta<PasoViatico>(respuesta.Mensaje,TAG);

            return new Respuesta<PasoViatico>(paso);
        }

        public override Respuesta<PasoViatico> ModificarPaso(PasoViatico paso, PasoViatico pasoOriginal,string subjectId)
        {

            var respuesta = Servicio.Modificar(paso, pasoOriginal, false, subjectId);

            if (respuesta.EsError)
                return new Respuesta<PasoViatico>(respuesta.Mensaje, TAG);

            return new Respuesta<PasoViatico>(paso);
        }
    }
}
