using System;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{
    public class Autorizacion :Seguimiento,  IAutorizacion
    {
        public int Id { get; set; }

        public int IdFlujo { get; set ; }

        public int Orden { get ; set; }

        public int IdRol { get ; set; }

        public string Sello { get ; set; }

        public int Estado { get ; set; }

        public string IdUsuarioAutorizacion { get; set; }

        public DateTime FechaAutorizacion { get; set; }

        public string IdUsuarioCancelacion { get; set; }

        public DateTime FechaCancelacion { get; set; }
       

        /*
        public string IdUsuarioCreo { get; set; }

        public DateTime FechaCreacion { get; set; }

       */

    }
}
