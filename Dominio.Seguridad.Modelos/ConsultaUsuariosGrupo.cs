using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class ConsultaUsuariosGrupo : IModel
    {
        public int IdGrupo { get; set; }
        public string Query { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronicoPersonal { get; set; }
        public string NumeroEmpleado { get; set; }
        public string CorreoElectronicoLaboral { get; set; }
        public string Area { get; set; }
        public string Dependencia { get; set; }
        public bool? EsHabilitado { get; set; }

        public bool? EsAsignado { get; set; }
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
