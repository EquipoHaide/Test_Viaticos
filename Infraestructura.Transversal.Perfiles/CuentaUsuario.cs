using System.Collections.Generic;
using System.Linq;

namespace Infraestructura.Transversal.Perfiles
{
    public class CuentaUsuario
    {
        public CuentaUsuario(string subjectid, List<PerfilUsuario> perfiles)
        {
            SubjectId = subjectid;
            Perfiles = perfiles;
            Perfiles.ForEach(p => p.Principal = false);
            /*Perfiles = perfiles.Select(p => new PerfilUsuario
            {
                SubjectId = p,
                Principal = false
            }).ToList();*/
        }

        public string SubjectId { get; set; }

        public List<PerfilUsuario> Perfiles { get; set; }

        public void EstablecerPrincipal(string subjectid)
        {
            Perfiles.Find(p => p.SubjectId == subjectid).Principal = true;
        }

        public PerfilUsuario PerfilPrincipal()
        {
            return Perfiles.Find(p => p.Principal);
        }
    }
}