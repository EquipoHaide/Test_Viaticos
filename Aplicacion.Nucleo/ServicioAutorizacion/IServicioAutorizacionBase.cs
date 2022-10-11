using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TAutorizacion,TQuery>
        where TAutorizacion : class,IAutorizacion
        where TQuery : class, IConsultaSolicitud 
    {
        public Respuesta<ConsultaPaginada<TAutorizacion>> Consultar(TQuery parametros, string subjectId);

        public Respuesta AdministrarAutorizaciones(List<TAutorizacion> autorizacones, string subjectId);
    }
}
