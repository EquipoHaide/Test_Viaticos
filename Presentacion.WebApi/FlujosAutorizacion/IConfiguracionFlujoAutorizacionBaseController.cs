using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using System.Collections.Generic;

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public interface IConfiguracionFlujoAutorizacionBaseController<TFlujo, TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {        
        //object ConsultarConfiguracionFlujo(TConsulta filtro);

        object Crear(ModeloConfiguracionFlujo<TPaso> config);

        object Modificar(ModeloConfiguracionFlujo<TPaso> config);

        object Eliminar(ModeloConfiguracionFlujo<TPaso> config);


    }
}