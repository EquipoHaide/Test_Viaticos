using System;
using Dominio.Nucleo;

namespace Aplicacion.Viaticos.Servicios
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IServicioFlujos, ServicioFlujos>();
        }
    }
}
