﻿using Dominio.Nucleo;
using Presentacion.WebApi.Modelos;
using System.Collections.Generic;

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public interface IConfiguracionFlujoAutorizacionBaseController<TPaso, TConsulta>        
        where TConsulta : IConsulta
        where TPaso : IPaso
    {        
        object ConsultarConfiguracionFlujo(TConsulta filtro);

        object Crear(ModeloConfiguracionFlujo<TPaso> config);
    }
}