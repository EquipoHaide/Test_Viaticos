using System;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Viaticos.Repositorios
{
    public interface IRepositorioPaso : IRepository<Entidades.PasoViatico>, IRepositorioPasoBase<Entidades.PasoViatico>
    {
   
    }
}
