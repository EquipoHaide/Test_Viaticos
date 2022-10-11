using System;
using Dominio.Nucleo;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;

namespace Infraestructura.Datos.Nucleo.AutorizacionSolicitud
{
    public abstract class RepositorioAutorizacion<TAutorizacion, TQuery> : Repository<TAutorizacion> , IRepositorioAutorizacion<TAutorizacion, TQuery>
        where TAutorizacion : class,IEntity
        where TQuery : class,IConsultaSolicitud 
    {
        public RepositorioAutorizacion(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract ConsultaPaginada<TAutorizacion> ConsultarAutorizaciones(TQuery parametros, string subjectId);
    }

}
