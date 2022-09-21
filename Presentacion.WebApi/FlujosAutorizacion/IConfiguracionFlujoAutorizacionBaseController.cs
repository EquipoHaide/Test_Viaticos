using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;
using System.Collections.Generic;

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public interface IConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {        
        //object ConsultarConfiguracionFlujo(TQuery filtro);

        object Crear([FromBody]ModeloConfiguracionFlujo<TFlujo, TPaso> config);

        object Modificar([FromBody]ModeloConfiguracionFlujo<TFlujo, TPaso> config);

        object Eliminar([FromBody]ModeloConfiguracionFlujo<TFlujo, TPaso> config);


    }
}