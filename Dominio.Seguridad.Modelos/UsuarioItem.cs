using Dominio.Nucleo;
using System;

namespace Dominio.Seguridad.Modelos
{
    public class UsuarioItem : IModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public bool EsHabilitado { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
