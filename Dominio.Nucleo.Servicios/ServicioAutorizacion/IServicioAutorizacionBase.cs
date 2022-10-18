using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion,TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class,IPaso
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada : class, ISolicitudCondensada
    {

        public Respuesta AdministrarAutorizacion(List<TInstanciaCondensada> instanciaCondensadas, List<TInstanciaCondensada> instanciaCondensadasOriginal, List<TAutorizacion> autorizaciones, List<TFlujo> flujos, int accion, string subjectId);
    }
}
