using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Transversal.Perfiles
{
    public interface IPerfilesManager
    {
        void EstablecerPrincipal(string subjectId, string principal);

        List<PerfilUsuario> ObtenerPerfiles(string subjectId);

        PerfilUsuario ObtenerPerfilPrincipal(string subjectId);

        Task CargarPerfilesUsuarioAsync(string subjectId, string accessToken);

        Task<List<EstadoPerfil>> IntrospeccionPerfilAsync(string subjectId, string accessToken);
    }
}