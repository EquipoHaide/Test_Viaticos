using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;

namespace Infraestructura.Datos.Nucleo.AutorizacionSolicitud
{
    public abstract class RepositorioAutorizacionBase<TInstanciaCondensada,TAutorizacion, TQuery> : Repository<TInstanciaCondensada> , IRepositorioAutorizacionBase<TInstanciaCondensada,TAutorizacion, TQuery>
        where TAutorizacion : class,IEntity
        where TInstanciaCondensada : class,IEntity
        where TQuery : class,IConsultaSolicitud 
    {
        public RepositorioAutorizacionBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract ConsultaPaginada<TInstanciaCondensada> ConsultarAutorizaciones(TQuery parametros, string subjectId);
        public abstract List<TAutorizacion> ObtenerAutorizacion(List<int> IdsAutorizacion);
    }

}
