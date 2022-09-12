using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Entidades;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Viaticos.Repositorios
{
    public interface IRepositorioConfiguracionFlujoViaticos : IRepository<FlujoViaticos>, IRepositorioConfiguracionFlujo<FlujoViaticos>
    {
    }

    
}
