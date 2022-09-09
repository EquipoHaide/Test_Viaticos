using System;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;

namespace Dominio.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IServicioFlujos<PasoViatico>, ServicioFlujos>();
            
        }
    }
}
