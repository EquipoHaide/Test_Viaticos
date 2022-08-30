using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class AccesoAsignado : IModel
    {
        public int IdAccion { get; set; }
        public bool EsAsignado { get; set; }
        public string Accion { get; set; }
        public string Ruta { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
