using Dominio.Nucleo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio.Seguridad.Entidades
{
    [Table("Usuarios", Schema = "Seguridad")]
    public class Usuario : IEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string SubjectId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }

        public string CorreoElectronicoPersonal { get; set; }
        [Required]
        public string CorreoElectronicoLaboral { get; set; }

        public string TelefonoPersonal { get; set; }
        [Required]
        public string TelefonoLaboral { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string NumeroEmpleado { get; set; }
        [Required]
        public string AreaAdscripcion { get; set; }
        [Required]
        public string Dependencia { get; set; }
        [Required]
        public bool EsCliente { get; set; }
        [Required]
        public bool EsHabilitado { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        public int SesionesPermitidas { get; set; } = 1;//Esto deberia de ser una regla de negocio configurable, Si no encuentro mas configuraciones se quedara atravez de alguna variable de config.

        public List<RolUsuario> Roles { get; set; }
        public List<UsuarioGrupo> Grupos { get; set; }
        public List<Sesion> Sesiones { get; set; }
    }
}
