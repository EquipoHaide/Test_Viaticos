﻿using System;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    public class SolicitudCondensada : SeguimientoCreacion, ISolicitudCondensada
    {
        public int Id { get; set; }

        public int IdAutorizacion { get; set; }

        public string Folio { get; set; }

        public string Concepto { get; set; }

        public int Estado { get; set; }

        public int Orden { get; set; }

        public int IdRol { get; set; }

        public string IdUsuarioAutorizacion { get; set; }

        public DateTime FechaAutorizacion { get; set; }

        public string IdUsuarioCancelacion { get; set; }

        public DateTime FechaCancelacion { get; set; }

        public DateTime FechaAfectacion { get; set; }


    }
}
