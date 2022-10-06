using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioFlujos<TFlujo,TPaso> : IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {
       
        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, bool validacionExtra, string subjectId);

        //public Respuesta<TFlujo> Modificar(TFlujo flujo,TFlujo flujoOrigial, bool validacionExtra, string subjectId);

        //public Respuesta<TFlujo> Eliminar(TFlujo flujo, List<TFlujo> listaFlujos, bool validacionExtra, string subjectId);
     
    }
}
