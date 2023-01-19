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
        public int IdEntePublico { get; set; }
        /// <summary>
        /// EL TIPO FLUJO COMO MANERA DE BUSQUEDA NO ESTOY MUY SEGURA DE QUE SE COLOQUE EN ESTE NIVEL DE CONTROL, CONSIDERO QUE PUEDA ESTAR TIPADO DENTRO DE LA INTERFAZ 
        /// </summary>
        public int TipoFlujo { get; set; }
        public DateTime FechaAfectacion { get; set; }
    }
}
