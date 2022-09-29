using System;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios.ConfiguracionFlujo
{
    public interface IRepositorioPasoBase<TPaso> : IRepository<TPaso>
        where TPaso : class,IEntity
    {

    }
}
