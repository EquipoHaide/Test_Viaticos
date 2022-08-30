﻿using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class ConsultaRol : IModel
    {
        public int IdUsuario { get; set; }
        public string Query { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? EsAsignado { get; set; }
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
