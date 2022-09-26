using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Nucleo
{
    public class NivelEmpleado : INivelEmpleado
    {
        public int Id { get; set; }

        [Required]
        public string Nivel { get; set; }
    }
}
