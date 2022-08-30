using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo
{
    public interface IServicioRecursoBase
    {
        Respuesta<List<IPermisoModel>> AdministrarRecursos(IEnumerable<IPermisoModel> permisos, string subjectId);
        Respuesta<ConsultaPaginada<IPermisoModel>> ConsultarRecursos(IModeloConsultaRecurso parametros, string idUsuario);
    }
}