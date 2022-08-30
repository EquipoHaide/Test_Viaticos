
namespace Dominio.Seguridad.Modelos
{
    public class UsuarioDeGrupoResumen : UsuarioDeGrupoBase
    {
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronicoPersonal { get; set; }
        public string CorreoElectronicoLaboral { get; set; }
        public string NumeroEmpleado { get; set; }
        public string Area { get; set; }
        public string Dependencia { get; set; }
        public bool? EsHabilitado { get; set; }
    }
}
