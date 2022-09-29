using System;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;

namespace Infraestructura.Datos.Nucleo.ConfiguracionFlujo
{
    public class RepositorioPasoBase<TPaso> : Repository<TPaso>, IRepositorioPasoBase<TPaso>
        where TPaso : class,IEntity
    {
        public RepositorioPasoBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
