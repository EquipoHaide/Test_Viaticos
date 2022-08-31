using System;
using AplicacionNucleo = Aplicacion.Nucleo;
using Dominio.Nucleo;
using Infraestructura.Transversal.Autenticacion;
using Infraestructura.Transversal.Plataforma.Extensiones;
using Infraestructura.Transversal.Plataforma.TinyIoC;
using Infraestructura.Transversal.Seguimiento;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;

using SeguridadAplicacion = Aplicacion.Seguridad.Servicios;
using SeguridadRepositorio = Infraestructura.Datos.Seguridad;
using SeguridadDominio = Dominio.Seguridad.Servicios;
using Infraestructura.Transversal.ServiciosExternos;
using Infraestructura.Transversal.Perfiles;


using ViaticosAplicacion = Aplicacion.Viaticos.Servicios;
using ViaticosDominio = Dominio.Viaticos.Servicios;
namespace Presentacion.WebApi.App
{
    public class AplicacionBase : AplicacionNucleo.IAplicacion
    {
        protected TinyIoCContainer Contenedor { get; set; }
        protected ApplicationLogger Logger { get; set; }
        protected TokenManager TokenManager { get; set; }
        protected PerfilesManager PerfilManager { get; set; }
        protected IConfiguration Configuration { get; set; }
        public bool AuthorizationEnabled { get; set; }
        public bool BasesDeDatosEnMemoria { get; private set; }
        MainMapper Mapper { get; set; }

        public virtual AplicacionNucleo.IAplicacion Inicializar(TinyIoCContainer contenedor, IConfiguration configuration)
        {
            Contenedor = contenedor;
            Configuration = configuration;

            AutoRegistrar();

            ConfigurarLogger();

            RegistTypes();

            if (MainMapper.Instance == null && !MainMapper.Inicializando)
            {
                Mapper = new MainMapper();

                MapingModels();
                Mapper.Inicializar();
            }

            return this;
        }

        public T Inject<T>(ref T value)
            where T : class
        {
            if (!Contenedor.CanResolve<T>()) return default(T);
            return Contenedor.Inject<T>(ref value);
        }

        public T Resolve<T>()
            where T : class
        {
            if (!Contenedor.CanResolve<T>()) return default(T);
            return Contenedor.Resolve<T>();
        }

        public void Register<TInterface, TService>()
            where TInterface : class
            where TService : class,
            TInterface
        {
            Contenedor.Register<TInterface, TService>().AsMultiInstance();
        }

        public void Register<TInterface>(TInterface serviceInstance)
            where TInterface : class
        {
            Contenedor.Register<TInterface>(serviceInstance);
        }

        public void AutoRegistrar()
        {
            Contenedor.Register<AplicacionNucleo.IAplicacion>(this);
            Contenedor.Register<IAplicacion>(this);
        }

        /// <summary>
        /// Configura las unidades de trabajo para trabajar en memoria
        /// </summary>
        public void UsarBasesDeDatosEnMemoria(string dbName = null)
        {
            /*dbName = dbName ?? Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<SeguridadUnidadDeTrabajo>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var ingresosOptions = new DbContextOptionsBuilder<IngresosUnidadDeTrabajo>()
                .UseInMemoryDatabase(dbName)
                .Options;

            var vehicularOptions = new DbContextOptionsBuilder<VehicularUnidadDeTrabajo>()
                .UseInMemoryDatabase(dbName)
                .Options;

            Contenedor.Register(options);
            Contenedor.Register(ingresosOptions);
            Contenedor.Register(vehicularOptions);

            BasesDeDatosEnMemoria = true;*/
        }

        /// <summary>
        /// Configura las unidades de trabajo para trabajar con SQL
        /// </summary>
        public void UsarBasesDeDatosSQL(string conexion)
        {

            /*var options = new DbContextOptionsBuilder<SeguridadUnidadDeTrabajo>()
                .UseSqlServer(conexion)
                .Options;

            var ingresosOptions = new DbContextOptionsBuilder<IngresosUnidadDeTrabajo>()
                .UseSqlServer(conexion)
                .Options;

            var vehicularOptions = new DbContextOptionsBuilder<VehicularUnidadDeTrabajo>()
                .UseSqlServer(conexion)
                .Options;

            Contenedor.Register(options);
            Contenedor.Register(ingresosOptions);
            Contenedor.Register(vehicularOptions);*/
        }

        private void ConfigurarLogger()
        {
            Logger = new ApplicationLogger()
            {
                LoggingLevelSwitch = new LoggingLevelSwitch(LogEventLevel.Verbose),
                Build = (loggingLevelSwitch) =>
                {
                    var logConfig = new LoggerConfiguration()
                    .MinimumLevel.ControlledBy(loggingLevelSwitch)
                    .Enrich.FromLogContext()
                    //.WriteTo.Async(config => config.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}"));
                    .WriteTo.Async(config =>
                    config.File("Logs/log.txt",
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message:lj} {Properties} {Exception}{NewLine}",
                        rollingInterval: RollingInterval.Day));

                    return logConfig.CreateLogger();
                }
            };

            Contenedor.Register<IApplicationLogger>(Logger);
        }

        /// <summary>
        /// Permite cambiar el nivel minimo del log.
        /// </summary>
        public void SwitchLogging(LogEventLevel level)
        {
            Logger.LoggingLevelSwitch.MinimumLevel = level;
        }

        private void MapingModels()
        {
            Dominio.Seguridad.Modelos.SeguridadProfileMapper.CreateMap(Mapper);
        }

        private void RegistTypes()
        {
            new SeguridadAplicacion.SeguridadMapperServices().Maping(this);
            new SeguridadRepositorio.SeguridadMapperServices().Maping(this);
            new SeguridadDominio.SeguridadMapperServices().Maping(this);

            new ViaticosAplicacion.ViaticosMapperServices().Maping(this);
            new ViaticosDominio.ViaticosMapperServices().Maping(this);

            new ServiciosExternosMapperServices().Maping(this);
        }

        public IApplicationLogger GetLogger()
        {
            return Logger;
        }

        public ITokenManager GetTokenManager()
        {
            return TokenManager;
        }

        public IPerfilesManager GetPerfilManager()
        {
            return PerfilManager;
        }

        public IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        public void Authentication(string authority, string clientId, string clientSecret, bool requireHttps = true)
        {
            TokenManager = new TokenManager();

            _ = TokenManager.ConfigureAsync(authority, clientId, clientSecret, requireHttps);

            Contenedor.Register<ITokenManager>(TokenManager);
        }

        public void Perfiles(ApiPerfil apiAutoridad, ApiPerfil apiPerfilesCuenta)
        {
            PerfilManager = new PerfilesManager();
            PerfilManager.Configurar(apiAutoridad, apiPerfilesCuenta, TokenManager);
            Contenedor.Register<IPerfilesManager>(PerfilManager);
        }
    }
}
