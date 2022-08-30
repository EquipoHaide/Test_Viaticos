
using Dominio.Nucleo;

namespace Dominio.Seguridad.Servicios
{
    public class SeguridadMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<Acciones.IServicioAcciones, Acciones.ServicioAcciones>();
            app.Register<Grupos.IServicioGrupos, Grupos.ServicioGrupos>();
            app.Register<Roles.IServicioRoles, Roles.ServicioRoles>();
            app.Register<Usuarios.IServicioUsuarios, Usuarios.ServicioUsuarios>();
        }
    }
}
