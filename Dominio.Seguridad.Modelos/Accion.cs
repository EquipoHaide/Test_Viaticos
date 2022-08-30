using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Seguridad.Modelos
{
    public class Accion : IModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
