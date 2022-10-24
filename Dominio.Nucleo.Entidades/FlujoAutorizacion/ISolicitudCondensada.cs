using Dominio.Nucleo.Firma;
using System;
namespace Dominio.Nucleo.Entidades
{
    public interface ISolicitudCondensada : IEntity, IQuien
    {
        public int    IdAutorizacion { get; set; }
        
        public string Folio { get; set; }
        public string Concepto { get; set; }
        public int    Estado { get; set; }
        public int    Orden { get; set; }
        public int    IdRol { get; set; }
        public DateTime FechaAfectacion { get; set; }

        public bool AplicaFirma { get; set; }
        
        public RecursosFirma RecursosFirma { get; set; }


    }
}
