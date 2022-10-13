using System;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Servicios;

namespace Dominio.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IServicioFlujos<Dominio.Viaticos.Entidades.FlujoViatico, Dominio.Viaticos.Entidades.PasoViatico>, ServicioFlujos>();

            app.Register<IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion>, ServicioAutorizacionViaticos>();


        }
    }
}
