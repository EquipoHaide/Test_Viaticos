using System;
using Dominio.Nucleo;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Datos.Seguridad.Repositorios;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;

namespace Infraestructura.Datos.Seguridad
{
    public class SeguridadMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<ISeguridadUnidadDeTrabajo, SeguridadUnidadDeTrabajo>();

            app.Register<IRepositorioAcciones, RepositorioAcciones>();
            app.Register<IRepositorioAccesos, RepositorioAccesos>();
            app.Register<IRepositorioGrupos, RepositorioGrupos>();
            app.Register<IRepositorioRoles, RepositorioRoles>();
            app.Register<IRepositorioUsuarios, RepositorioUsuarios>();
        }
    }
}