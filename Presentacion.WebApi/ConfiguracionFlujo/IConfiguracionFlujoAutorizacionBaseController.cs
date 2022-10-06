using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;
using System.Collections.Generic;

namespace Presentacion.WebApi.ConfiguracionFlujo 
{
    public interface IConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso, TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class, IConsultaFlujo
    {        
        object ConsultarConfiguracionFlujo(TQuery filtro);

        object AdministrarFlujos([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config);

        //object Modificar([FromBody] ModeloConfiguracionFlujo<TFlujo, TPaso> config);

        //object Eliminar([FromQuery] List<int> id );

        

    }
}