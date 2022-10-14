using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso> : IServicioAutorizacionBase<TInstanciaCondensada, TAutorizacion,TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada : class, ISolicitudCondensada
    {
        public Respuesta<List<TInstanciaCondensada>> AdministrarAutorizacion(List<TInstanciaCondensada> autorizaciones, string subjectId)
        {
            return new Respuesta<List<TInstanciaCondensada>>("");
        }
    }
}
