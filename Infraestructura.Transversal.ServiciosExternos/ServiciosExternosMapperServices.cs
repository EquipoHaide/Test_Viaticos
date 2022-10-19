using Aplicacion.Nucleo.Firma;
using Dominio.Nucleo;
using Infraestructura.Transversal.ServiciosExternos.FirmaElectronica;

namespace Infraestructura.Transversal.ServiciosExternos
{
    public class ServiciosExternosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IServicioFirmaElectronica, ServicioFirmaElectronica>();
        }
    }
}