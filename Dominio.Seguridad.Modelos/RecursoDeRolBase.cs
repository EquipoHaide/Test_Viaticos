using Dominio.Nucleo;
using System;

namespace Dominio.Seguridad.Modelos
{
    public class RecursoDeRolBase : PermisoBase, IPermisoModel
    {
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
