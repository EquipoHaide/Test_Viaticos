using Dominio.Nucleo;
using System;


namespace Dominio.Seguridad.Modelos
{
    public class RecursoDeAccionBase : PermisoBase, IPermisoModel
    {
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
