using Aplicacion.Nucleo;
using Dominio.Seguridad.Modelos;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;


namespace Aplicacion.Seguridad.Servicios
{
    public interface IServicioAcciones : IServicioRecursoBase
    {
        Respuesta<bool> Validar(string accion, string subjectId);
        Respuesta<List<AccesoAsignado>> Validar(List<string> acciones, string subjectId);
        Respuesta<List<Modulo>> ObtenerModulos(string subjectId);
        Respuesta<ConsultaPaginada<Acceso>> ConsultarAccesos(ConsultaAcceso parametros, string subjectId);
        Respuesta<List<Acceso>> AdministrarAccesos(IEnumerable<Acceso> accesos, string subjectId);

    }
}
