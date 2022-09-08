using System;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    //Esta clase debe de heredar la interfaz del api generica de flujos de autorizacion
    public class ConsultaConfiguracionFlujo : IConsultaFlujo
    {
        public string Query { get; set; } = "";
        public int Pagina { get; set; } = 1;
        public int ElementosPorPagina { get; set; } = 20;

        public string Query2 { get; set; } = "";
    }
}
