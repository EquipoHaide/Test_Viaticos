using Aplicacion.Nucleo;
using Infraestructura.Transversal.Plataforma.TinyIoC;
using Microsoft.Extensions.Configuration;

namespace Presentacion.WebApi.App
{
    public class Aplicacion : AplicacionBase, IAplicacion
    {
        private static Aplicacion instancia;
        public static Aplicacion Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Aplicacion();
                }

                return instancia;
            }
        }
        private Aplicacion() { }

        public override IAplicacion Inicializar(TinyIoCContainer contenedor, IConfiguration configuration)
        {
            base.Inicializar(contenedor, configuration);
            return this;
        }
    }
}
