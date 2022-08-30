using Dominio.Nucleo;
using Dominio.Seguridad.Entidades;
using System.Collections.Generic;

namespace Dominio.Seguridad.Modelos
{
    public class Modulo : IModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<OpcionModulo> Opciones { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
