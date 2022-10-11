using System;

using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios.ConfiguracionFlujo
{
    public interface IRepositorioAutorizacion<TAutorizacion, TQuery> : IRepository<TAutorizacion>
        where TAutorizacion : class, IEntity
        where TQuery : class, IConsultaSolicitud 
    {
        
        ConsultaPaginada<TAutorizacion> ConsultarAutorizaciones(TQuery parametros, string subjectId);
    }
}
