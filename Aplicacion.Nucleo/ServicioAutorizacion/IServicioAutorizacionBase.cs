using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TSolicitudCondensada, TAutorizacion,TFlujo,TPaso,TQuery>

        where TAutorizacion : class,IAutorizacion
        where TSolicitudCondensada : class,ISolicitudCondensada
        where TQuery : class, IQuery
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
        public Respuesta<ConsultaPaginada<TSolicitudCondensada>> Consultar(TQuery parametros, string subjectId);

        public Respuesta AdministrarAutorizaciones(List<TSolicitudCondensada> Autorizacones, List<TFlujo> flujos, int Accion, string subjectId);

        public Respuesta AdministracionFinalAutorizacion(List<TSolicitudCondensada> solicitudes, List<TAutorizacion> autorizaciones, string subjectId);
    }
}
