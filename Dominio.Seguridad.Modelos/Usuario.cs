using Dominio.Nucleo;
using System;


namespace Dominio.Seguridad.Modelos
{  
        public class Usuario : IModel
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string CorreoElectronicoPersonal { get; set; }
            public string CorreoElectronicoLaboral { get; set; }
            public string TelefonoPersonal { get; set; }
            public string TelefonoLaboral { get; set; }
            public string Extension { get; set; }
            public string NombreUsuario { get; set; }
            public string NumeroEmpleado { get; set; }
            public string AreaAdscripcion { get; set; }
            public string Dependencia { get; set; }
            public bool EsCliente { get; set; }
            public DateTime FechaCreacion { get; set; } = DateTime.Now;

            public bool IsValid()
            {
                throw new NotImplementedException();
            }

            public bool Sincronizar(Entidades.Usuario usuario)
            {
                bool cambio = false;
                cambio |= usuario.Nombre != this.Nombre;
                cambio |= usuario.Apellidos != this.Apellidos;
                cambio |= usuario.CorreoElectronicoPersonal != this.CorreoElectronicoPersonal;
                cambio |= usuario.CorreoElectronicoLaboral != this.CorreoElectronicoLaboral;
                cambio |= usuario.TelefonoPersonal != this.TelefonoPersonal;
                cambio |= usuario.TelefonoLaboral != this.TelefonoLaboral;
                cambio |= usuario.Extension != this.Extension;
                cambio |= usuario.NombreUsuario != this.NombreUsuario;
                cambio |= usuario.NumeroEmpleado != this.NumeroEmpleado;
                cambio |= usuario.AreaAdscripcion != this.AreaAdscripcion;
                cambio |= usuario.Dependencia != this.Dependencia;

                usuario.Nombre = this.Nombre;
                usuario.Apellidos = this.Apellidos;
                usuario.CorreoElectronicoPersonal = this.CorreoElectronicoPersonal;
                usuario.CorreoElectronicoLaboral = this.CorreoElectronicoLaboral;
                usuario.TelefonoPersonal = this.TelefonoPersonal;
                usuario.TelefonoLaboral = this.TelefonoLaboral;
                usuario.Extension = this.Extension;
                usuario.NombreUsuario = this.NombreUsuario;
                usuario.NumeroEmpleado = this.NumeroEmpleado;
                usuario.AreaAdscripcion = this.AreaAdscripcion;
                usuario.Dependencia = this.Dependencia;

                return cambio;
            }
        }
    }

