using System;
using System.Collections.Generic;

namespace Dominio.Nucleo
{
    public class ResumenInformacion
    {
        public int RegistrosExitosos { get; set; }
        public int RegistrosFallidos { get; set; }
        public List<DetalleConfiguracion> DetallesConfiguracion { get; set; }
    }

    public class DetalleConfiguracion
    {
        //public DetalleConfiguracion(int id, string nombre, string motivo)
        //{
        //    this.Id = id;
        //    //this.Nombre = nombre;
        //    this.Motivo = motivo;
        //}

        //public int Id { get; set; }
        ////public string Nombre { get; set; }
        //public string Motivo { get; set; }
        public List<DetallesFlujo> DetalleFlujo { get; set; }
        public List<DetallePaso> DetallePasos { get; set; }


    }

    public class DetallesFlujo
    {
        public DetallesFlujo(int id, string nombre, string motivo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Motivo = motivo;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Motivo { get; set; }
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
