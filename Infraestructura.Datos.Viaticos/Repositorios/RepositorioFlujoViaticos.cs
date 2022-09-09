using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViaticos>, IRepositorioViaticos
    {
        public RepositorioFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }
    }
}
