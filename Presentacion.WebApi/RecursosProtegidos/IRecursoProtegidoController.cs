using Dominio.Nucleo;
using System.Collections.Generic;

namespace Presentacion.WebApi.RecursosProtegidos
{
    public interface IRecursoProtegidoController<TPermiso, TConsulta> 
        where TPermiso : PermisoBase, IPermisoModel
        where TConsulta : IModeloConsultaRecurso
    {        
        object ConsultarRecursos(TConsulta filtro);

        object AdministrarRecursos(List<TPermiso> permisos);
    }
}