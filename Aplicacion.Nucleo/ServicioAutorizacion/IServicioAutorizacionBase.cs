using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion,TQuery>
        where TAutorizacion : class,IAutorizacion
        where TInstanciaCondensada : class,IInstanciaCondensada
        where TQuery : class, IConsultaSolicitud 
    {
        public Respuesta<ConsultaPaginada<TInstanciaCondensada>> Consultar(TQuery parametros, string subjectId);

        public Respuesta AdministrarAutorizaciones(List<TInstanciaCondensada> Autorizacones, int Accion, string subjectId);
    }
}
