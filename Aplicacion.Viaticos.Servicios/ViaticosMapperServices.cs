using System;
using Aplicacion.Viaticos.Servicios.ConfiguracionFlujos;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {

            app.Register<IServicioFlujo<Dominio.Viaticos.Entidades.FlujoViaticos, Dominio.Viaticos.Entidades.PasoViatico>, ServicioFlujo>();

        }
    }
}
