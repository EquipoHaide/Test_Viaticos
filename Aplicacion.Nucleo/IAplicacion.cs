using Infraestructura.Transversal.Autenticacion;
using Infraestructura.Transversal.Seguimiento;
using Serilog.Events;
using Microsoft.Extensions.Configuration;
using Infraestructura.Transversal.Perfiles;

namespace Aplicacion.Nucleo
{
    public interface IAplicacion : Dominio.Nucleo.IAplicacion
    {
        bool BasesDeDatosEnMemoria { get; }
        /// <summary>
        /// Obtiene la intancia de un logger.
        /// </summary>
        IApplicationLogger GetLogger();

        /// <summary>
        /// Obtiene la intancia de un token manager.
        /// </summary>
        ITokenManager GetTokenManager();

        IPerfilesManager GetPerfilManager();

        IConfiguration GetConfiguration();

        /// <summary>
        /// Permite cambiar el nivel minimo del log.
        /// </summary>
        void SwitchLogging(LogEventLevel level);

        /// <summary>
        /// Configura las unidades de trabajo para trabajar en memoria
        /// </summary>
        void UsarBasesDeDatosEnMemoria(string dbName = null);

        /// <summary>
        /// Configura las unidades de trabajo para trabajar con SQL
        /// </summary>
        void UsarBasesDeDatosSQL(string conexion = null);

        void Authentication(string authority, string clientId, string clientSecret, bool requireHttps = true);
    }
}