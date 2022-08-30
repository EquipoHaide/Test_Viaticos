using Dominio.Nucleo;

namespace Dominio.Seguridad.Modelos
{
    public class ConsultaGrupo : IModel
    {
        public int IdGrupo { get; set; }
        public string Query { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Pagina { get; set; }
        public int ElementosPorPagina { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
