using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion,TFlujo,TPaso,TQuery>

        where TAutorizacion : class,IAutorizacion
        where TInstanciaCondensada : class,ISolicitudCondensada
        where TQuery : class, IQuery
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
        public Respuesta<ConsultaPaginada<TInstanciaCondensada>> Consultar(TQuery parametros, string subjectId);

        public Respuesta AdministrarAutorizaciones(List<TInstanciaCondensada> Autorizacones, List<TFlujo> flujos, int Accion, string subjectId);
    }
}
