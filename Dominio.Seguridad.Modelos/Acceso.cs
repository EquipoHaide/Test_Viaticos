using Dominio.Nucleo;
using System;


namespace Dominio.Seguridad.Modelos
{
    public class Acceso : IModel
    {
        public int Id { get; set; }
        public int IdAccion { get; set; }
        public int IdRol { get; set; }
        public bool EsAsignado { get; set; }
        public DateTime? FechaCaducidad { get; set; }

        public string Modulo { get; set; }
        public string Opcion { get; set; }
        public string Accion { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
