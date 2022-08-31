using System;

namespace Dominio.Viaticos.Modelos
{
    //Esta clase debe de heredar la interfaz del api generica de flujos de autorizacion
    public class ConsultaConfiguracionFlujo
    {
        public string Query { get; set; } = "";
        public int Pagina { get; set; } = 1;
        public int ElementosPorPagina { get; set; } = 20;
    }
}
