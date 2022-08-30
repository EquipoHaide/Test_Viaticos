using Dominio.Nucleo;

namespace Aplicacion.Seguridad.Servicios
{
    public class SeguridadMapperServices : MapperServices
    {
            public override void Maping(IAplicacion app)
        {
            app.Register<IServicioAcciones, ServicioAcciones>();
            app.Register<IServicioGrupos, ServicioGrupos>();
            app.Register<Core.IServicioRoles, ServicioRoles>();
            app.Register<IServicioUsuarios, ServicioUsuarios>();
        }
    }
}
