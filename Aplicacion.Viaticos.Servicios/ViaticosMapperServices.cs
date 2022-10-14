using System;
using Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;
using Aplicacion.Viaticos.Servicios.AutorizacionViaticos;

using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Entidad = Dominio.Viaticos.Entidades;

namespace Aplicacion.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {

            app.Register<IServicioFlujo<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico, ConsultaConfiguracionFlujo>, ServicioFlujo>();
            app.Register<IServicioAutorizacionViaticos<Entidad.SolicitudCondensada,Entidad.Autorizacion,Entidad.FlujoViatico,Entidad.PasoViatico,Dominio.Viaticos.Modelos.ConsultaSolicitudes>, ServicioAutorizacionViaticos>();
            

        }
    }
}
