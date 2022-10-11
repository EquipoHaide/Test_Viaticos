using System;
using Dominio.Nucleo.FlujoAutorizacion;

namespace Dominio.Viaticos.Modelos
{
    public class ConsultaSolicitudes : IConsultaSolicitud
    {
        public string Folio { get; set; }
        public int IdEntePublico { get; set; }
        public int Estatus { get; set; }
        public string Query { get; set; }
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }
    }



}
