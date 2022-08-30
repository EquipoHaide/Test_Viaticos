using Dominio.Nucleo;
using System.Collections.Generic;


namespace Dominio.Seguridad.Modelos
{
    public class OpcionModulo : IModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Accion> Acciones { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
