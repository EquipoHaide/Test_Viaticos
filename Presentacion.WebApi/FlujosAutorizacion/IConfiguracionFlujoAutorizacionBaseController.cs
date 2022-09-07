using Dominio.Nucleo;
using System.Collections.Generic;

namespace Presentacion.WebApi.RecursosProtegidos
{
    public interface IConfiguracionFlujoAutorizacionBaseController<TFlujo, TConsulta>        
        where TConsulta : IConsulta
        //where TFlujo : IFlujoNew
    {        
        object ConsultarConfiguracionFlujo(TConsulta filtro);

        object Crear(List<TFlujo> flujos);
    }
}