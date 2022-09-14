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

        object Crear(TFlujo config);

        object Modificar(TFlujo config);

        object Eliminar(TFlujo config);


    }
}