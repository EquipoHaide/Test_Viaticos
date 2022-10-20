using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Dominio.Nucleo.Entidades
{
    public interface IFlujo<TPaso> : IEntity ,ISeguimiento 
       where TPaso : IPaso
    {
        public int IdTipoEnte { get; set; }
    
        public int? IdNivelEmpleado { get; set; }

        /// <summary>
        /// Es un Enumerable
        /// 1 --> Predeterminado
        /// 2 --> Particular
        /// </summary>
        public int TipoFlujo { get; set; }

        List<TPaso> Pasos { get; set; }



    }

}
