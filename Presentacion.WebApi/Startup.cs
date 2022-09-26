using Aplicacion.Nucleo;
using Infraestructura.Transversal.Perfiles;
using Infraestructura.Transversal.Plataforma.TinyIoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentacion.WebApi.AppCircuitBreaker;
using Presentacion.WebApi.HealthCheck;
using Presentacion.WebApi.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace Presentacion.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = configBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Auth:Authority"];
                    options.ApiName = Configuration["Auth:ApiName"];
                    options.RequireHttpsMetadata = bool.Parse(Configuration["Auth:RequireHttpsMetadata"]);

                    options.TokenRetriever = TokenRetrieval.Mixed();
                    options.SaveToken = true;
                });

            //Circuit Breaker
            CircuitBreaker circuitBreaker = new CircuitBreaker();
            services.AddSingleton<ICircuitBreaker>(circuitBreaker);

            //Controllers
            services.AddControllers(options =>
            {
                options.Filters.Add<CircuitBreakerResourceFilter>();
            });

            

            //health check
            services.AddHealthChecks().AddTypeActivatedCheck<AppHealthCheck>("health", args: new object[] { circuitBreaker });

            //Aplicacion
            var contenedor = new TinyIoCContainer();
            contenedor.AutoRegister();//inicializar contenedor

            App.Aplicacion.Instancia
                      .Inicializar(contenedor, Configuration)
                      .UsarBasesDeDatosSQL(Configuration.GetConnectionString("sit"));
        
            App.Aplicacion.Instancia.Authentication(Configuration["Auth:Authority"], Configuration["Auth:ClientId"], Configuration["Auth:ClientSecret"], bool.Parse(Configuration["Auth:RequireHttpsMetadata"]));
            App.Aplicacion.Instancia.AuthorizationEnabled = true;

            //perfiles manager
            var autoridad = Configuration.GetSection("Perfiles").GetSection("Autoridad") as ApiPerfil;
            var perfilesCuenta = Configuration.GetSection("Perfiles").GetSection("PerfilesCuenta") as ApiPerfil;
            App.Aplicacion.Instancia.Perfiles(autoridad, perfilesCuenta);
            //

            services.AddSingleton<IAplicacion>(App.Aplicacion.Instancia);

            //Cross Origin
            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    policy =>
                    {
                        policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                    });
            });

            //Circuit Breaker Up
            circuitBreaker.Up();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks(AppHealthCheckConstants.Path);
            });
        }
    }
}
