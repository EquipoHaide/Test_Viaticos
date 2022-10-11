using System;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    public class InstanciaCondensada : IInstanciaCondensada
    {
        public int Id { get; set; }

        public int IdAutorizacion { get ; set; }

        public string Quien { get ; set; }

        public string Folio { get ; set; }

        public string Concepto { get ; set; }

        public int Estado { get ; set; }

        public int Orden { get ; set; }

        public int IdRol { get ; set; }

    }
}
