using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios.ConfiguracionFlujo
{
    public interface IRepositorioAutorizacionBase<TInstanciaCondesada, TAutorizacion,TQuery> : IRepository<TInstanciaCondesada>
        where TAutorizacion : class, IEntity
        where TInstanciaCondesada : class, IEntity
        where TQuery : class, IConsultaSolicitud 
    {
        
        ConsultaPaginada<TInstanciaCondesada> ConsultarAutorizaciones(TQuery parametros, string subjectId);


        List<TAutorizacion> ObtenerAutorizacion(List<int> IdsAutorizacion);
        
    }
}
