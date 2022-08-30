
using System;
using System.Collections.Generic;

namespace Infraestructura.Transversal.Perfiles
{
    public class PerfilUsuario
    {
        public string SubjectId { get; set; }

        public string NumeroEmpleado { get; set; }

        public string Rfc { get; set; }

        public string Curp { get; set; }

        public string Sexo { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Nombre { get; set; }

        public string Trato { get; set; }

        public string Puesto { get; set; }

        public string DescripcionPuesto { get; set; }

        public string Nivel { get; set; }

        public DateTime FechaIngreso { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public string CorreoElectronico { get; set; }

        public string ClaveRamo { get; set; }

        public string Ramo { get; set; }

        public string ClaveUnidad { get; set; }

        public string Unidad { get; set; }

        public List<string> Vinculados { get; set; }

        public List<int> Entes { get; set; }

        public bool Principal { get; set; }
    }
}
