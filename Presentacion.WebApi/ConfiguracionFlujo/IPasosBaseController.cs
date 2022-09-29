using System;
using Dominio.Nucleo.Entidades;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;

namespace Presentacion.WebApi.ConfiguracionFlujo
{
    public interface IPasosBaseController<TPaso> 
        where TPaso : class, IPaso
    {
        
        object Modificar([FromBody] ModeloPaso<TPaso> config);

        object Eliminar([FromQuery] int id);

    }
}
