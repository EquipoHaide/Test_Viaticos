using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioFlujos<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
       
        public Respuesta<TFlujo> Crear(TFlujo flujo, bool validacionExtra, string subjectId);

        public Respuesta<TFlujo> Modificar(TFlujo flujo, bool validacionExtra, string subjectId);

        public Respuesta<TFlujo> Eliminar(TFlujo flujo, bool validacionExtra, string subjectId);
     
    }
}
