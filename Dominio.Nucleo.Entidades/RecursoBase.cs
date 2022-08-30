using System.Collections.Generic;

namespace Dominio.Nucleo.Entidades
{
    public class RecursoBase<TPermiso> : Seguimiento, IEntity
        where TPermiso : Permiso, new()
    {
        public int Id { get; set; }

        public List<TPermiso> Recursos { get; set; }

        public void CrearRecursos(string subjectId, IEnumerable<IRolItem> roles)
        {
            if (Recursos == null) Recursos = new List<TPermiso>();

            if (roles != null)
            {
                foreach (var r in roles)
                {
                    TPermiso recurso = new TPermiso()
                    {
                        IdRol = r.IdRol,
                        IdRecurso = this.Id,
                        EsLectura = true,
                        EsEscritura = true,
                        EsEjecucion = true
                    };
                    recurso.Seguir(subjectId);
                    Recursos.Add(recurso);
                }
            }
        }
    }
}