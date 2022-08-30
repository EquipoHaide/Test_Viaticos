using Dominio.Nucleo;
using System;

namespace Dominio.Seguridad.Modelos
{
    public class RecursoDeGrupoBase : PermisoBase, IPermisoModel
    {
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
