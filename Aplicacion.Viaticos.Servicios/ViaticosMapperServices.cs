using System;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Aplicacion.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IServicioFlujos, ServicioFlujos>();
            app.Register<IServicioFlujosNew<FlujoViaticos>, ServicioFlujosNew>();
            app.Register<ConfiguracionFlujos.IServicioFlujo, ConfiguracionFlujos.ServicioFlujo<IPaso>>();
        }
    }
}
