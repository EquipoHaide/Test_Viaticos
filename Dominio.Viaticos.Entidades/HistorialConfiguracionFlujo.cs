
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;

namespace Dominio.Viaticos.Entidades
{

    /// <summary>
    /// Configuracion del Flujo 
    /// </summary>
    [Table("HistorialesFlujos", Schema = "Viaticos")]
    public class HistorialFlujoViatico : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdFlujo { get; set; }

        [Required]
        public int IdTipoEnte { get; set; }


        public int? IdNivelEmpleado { get; set; }

        [Required]
        public int TipoFlujo { get; set; }

        [ForeignKey("IdFlujo")]
        public FlujoViatico FlujoViatico { get; set; }

        [Required]
        public string IdUsuarioModifico { get; set; }
        [Required]
        public DateTime OperacionInicio { get; set; }
        [Required]
        public DateTime? OperacionFin { get; set; }






    }




}
