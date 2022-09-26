using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase<Dominio.Viaticos.Entidades.ConfiguracionFlujo, Dominio.Viaticos.Entidades.PasoViatico>,
        IServicioFlujos<Dominio.Viaticos.Entidades.ConfiguracionFlujo, Dominio.Viaticos.Entidades.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public Respuesta<ConfiguracionFlujo> Crear(ConfiguracionFlujo flujo, bool validacionExtra, string subjectId)
        {
            if (flujo.NombreFlujo == null)
                return new Respuesta<ConfiguracionFlujo>("Es necesario una descripcion en el flujo", TAG);

            if(!flujo.Activo)
                return new Respuesta<ConfiguracionFlujo>("Es necesario activar el flujo", TAG);

            var controlFlujo = new ConfiguracionFlujo() {
                IdEntePublico = flujo.IdEntePublico,
                IdNivelEmpleado = flujo.IdNivelEmpleado,
                TipoFlujo = flujo.TipoFlujo,
                NombreFlujo = flujo.NombreFlujo,
                Activo = flujo.Activo,
                Pasos = flujo.Pasos
            };

            return new Respuesta<ConfiguracionFlujo>(controlFlujo);
        }

        public Respuesta<ConfiguracionFlujo> Eliminar(ConfiguracionFlujo flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<ConfiguracionFlujo>(flujo);
        }

        public Respuesta<ConfiguracionFlujo> Modificar(ConfiguracionFlujo flujo, bool validacionExtra, string subjectId)
        {
            return new Respuesta<ConfiguracionFlujo>(flujo);

        }
    }
}
