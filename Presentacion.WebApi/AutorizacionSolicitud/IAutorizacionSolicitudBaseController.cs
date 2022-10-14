using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.AutorizacionSolicitudes
{
    public interface IAutorizacionSolicitudBaseController<TInstanciaCondensada, TAutorizacion, TFlujo, TPaso, TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada : class, IInstanciaCondensada
        where TQuery : class,IQuery
    {

        object ConsultarSolicitudes([FromQuery] TQuery filtro);

        object AdministrarAutorizaciones([FromBody] ModeloSolicitud<TInstanciaCondensada,TFlujo,TPaso> modelo);
    }
}
