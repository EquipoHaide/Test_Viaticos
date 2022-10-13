using System;
namespace Dominio.Nucleo.FlujoAutorizacion
{
    public interface IConsultaSolicitud : IQuery
    {
        public string Folio { get; set; }
        public int IdEntePublico { get; set; }
        public int Estatus { get; set; }
   
    }
}
