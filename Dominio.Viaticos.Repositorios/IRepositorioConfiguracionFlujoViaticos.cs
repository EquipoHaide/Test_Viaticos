using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Modelos;
using Dominio.Nucleo.Entidades;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Viaticos.Repositorios
{
   
    public interface IRepositorioConfiguracionFlujoViaticos : IRepository<FlujoViaticos>, IRepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>, PasoViatico>

    {
    }

    
}
