using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public class ResumenInformacion
    {
        public int RegistrosExitosos { get; set; }
        public int RegistrosFallidos { get; set; }
        public List<DetalleFlujo> DetalleFlujos { get; set; }
    }

    public class DetalleFlujo
    {
        public DetalleFlujo(int id, string nombre, string motivo)
        {
            this.Id = id;
            //this.Nombre = nombre;
            this.Motivo = motivo;
        }

        public int Id { get; set; }
        //public string Nombre { get; set; }
        public string Motivo { get; set; }
        public List<DetallePaso> DetallePasos { get; set; }


    }

    public class DetallePaso
    {
        public DetallePaso(int id, string nombre, string motivo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Motivo = motivo;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Motivo { get; set; }
    }
}
