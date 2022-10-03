using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;

namespace Infraestructura.Datos.Nucleo.ConfiguracionFlujo
{
    public abstract class RepositorioPasoBase<TPaso> : Repository<TPaso>, IRepositorioPasoBase<TPaso>
        where TPaso : class,IEntity
    {
        public RepositorioPasoBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract List<TPaso> ObtenerPasos(int idFlujo);
        public abstract List<TPaso> ObtenerPasosReordenar(int idPasoEliminado, int idFlujo);
    }
}
