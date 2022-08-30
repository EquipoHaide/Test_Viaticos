using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Seguridad.Modelos
{
    public class UsuarioDeGrupoBase
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }
        public bool EsAsignado { get; set; }
    }
}
