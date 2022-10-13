﻿using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.AutorizacionSolicitudes
{
    public interface IAutorizacionSolicitudBaseController<TInstanciaCondensada, TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada : class, IInstanciaCondensada
        where TQuery : class,IConsultaSolicitud
    {

        object ConsultarSolicitudes([FromQuery] TQuery filtro);

        object AdministrarAutorizaciones([FromBody] ModeloSolicitud<TInstanciaCondensada> modelo);
    }
}
