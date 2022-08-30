using IdentityModel.Client;
using Infraestructura.Transversal.Autenticacion;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructura.Transversal.Perfiles
{
    public class PerfilesManager : IPerfilesManager
    {
        private ITokenManager TokenManager { get; set; }
        private ApiPerfil Autoridad { get; set; }
        private ApiPerfil PerfilesCuenta { get; set; }
        private string uriRHcam => "perfiles/instrospeccion";
        private string uriApps => "usuarios/perfiles";

        List<CuentaUsuario> Cuentas { get; set; }

        public PerfilesManager()
        {
            Cuentas = new List<CuentaUsuario>();
        }

        public void Configurar(ApiPerfil apiAutoridad, ApiPerfil apiPerfilesCuenta, ITokenManager tokenManager)
        {            
            Autoridad = apiAutoridad;// configuration.GetSection("Perfiles").GetSection("Autoridad") as ApiPerfil;
            PerfilesCuenta = apiPerfilesCuenta;// configuration.GetSection("Perfiles").GetSection("PerfilesCuenta") as ApiPerfil;
            TokenManager = tokenManager;
        }

        public void EstablecerPrincipal(string subjectId, string principal)
        {
            var cuenta = Cuentas.Find(pc => pc.SubjectId == subjectId);

            if(cuenta != null)
            {
                if(cuenta.Perfiles.Any(p => p.SubjectId == principal))
                {
                    cuenta.Perfiles.ForEach(p => 
                    {
                        p.Principal = p.SubjectId == principal;
                    });
                }
            }
        }

        public List<PerfilUsuario> ObtenerPerfiles(string subjectId)
        {
            var cuenta = Cuentas.Find(pc => pc.SubjectId == subjectId);

            if (cuenta == null)
                return new List<PerfilUsuario>();

            return cuenta.Perfiles;
        }

        public PerfilUsuario ObtenerPerfilPrincipal(string subjectId)
        {
            var cuenta = Cuentas.Find(pc => pc.SubjectId == subjectId);

            if (cuenta == null)
                return null;

            return cuenta.PerfilPrincipal();
        }

        public async Task CargarPerfilesUsuarioAsync(string subjectId, string accessToken)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(PerfilesCuenta.UrlBase);

            var token = await TokenManager.GetTokenAsync(PerfilesCuenta.Scopes, subjectId, accessToken);
            client.SetBearerToken(token);

            var response = await client.GetAsync($"{uriApps}/{subjectId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var perfiles = JsonSerializer.Deserialize<RespuestaApi<List<PerfilUsuario>>>(content);

                if (perfiles.Success)
                {
                    var perfilCuenta = Cuentas.Find(pc => pc.SubjectId == subjectId);

                    if (perfilCuenta != null)
                    {
                        perfilCuenta = new CuentaUsuario(subjectId, perfiles.Content);
                        /*perfilCuenta.Perfiles = perfiles.Content.Select(p => new PerfilUsuario
                        {
                            SubjectId = p,
                            Principal = false
                        }).ToList();*/
                    }
                    else
                    {
                        Cuentas.Add(new CuentaUsuario(subjectId, perfiles.Content));
                    }
                }
            }
        }

        public async Task<List<EstadoPerfil>> IntrospeccionPerfilAsync(string subjectId, string accessToken)
        {
            var cuenta = Cuentas.Find(pc => pc.SubjectId == subjectId);

            if (cuenta == null)
                return null;

            var perfiles = cuenta.Perfiles.Select(p => p.SubjectId).ToList();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(Autoridad.UrlBase);

            var token = await TokenManager.GetTokenAsync(PerfilesCuenta.Scopes, subjectId, accessToken);
            client.SetBearerToken(token);

            string ids = string.Join(",", perfiles);

            var response = await client.GetAsync($"{uriRHcam}/{ids}");

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estados = JsonSerializer.Deserialize<RespuestaApi<List<EstadoPerfil>>>(content);

                if(estados.Success)
                {
                    return estados.Content;
                }

                return null;
            }

            return null;
        }
    }
}