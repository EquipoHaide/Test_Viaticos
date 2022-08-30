using Dominio.Seguridad.Repositorios;
using Infraestructura.Datos.Seguridad.UnidadDeTrabajo;
using MicroServices.Platform.Repository;

namespace Infraestructura.Datos.Seguridad.Repositorios
{
    public class RepositorioAccesos : Repository<Dominio.Seguridad.Entidades.Acceso>, IRepositorioAccesos
    {
        public RepositorioAccesos(ISeguridadUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }


    }
}
