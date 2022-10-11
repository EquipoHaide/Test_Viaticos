using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi.FlujoAutorizacion
{
    public interface IFlujoAutorizacionController<TAutorizacion, TQuery>
        where TAutorizacion : IAutorizacion
        where TQuery : IConsultaSolicitud
    {

        object ConsultarSolicitudes([FromQuery] TQuery filtro);

        object AdministrarAutorizaciones([FromBody] List<TAutorizacion> autorizacion);
    }
}
