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
       /// <summary>
       /// Este es un metodo que se agrego a manera de ejemplo, para demostrar que cada negocio podra crear o agregar sus propios metodos segun lo requiera.
       /// Este es un metodo a manera de ejemplo, pueden incluso modificar su firma.
       /// </summary>
       /// <param name="flujos"></param>
       /// <param name="validacionExtra"></param>
       /// <param name="subjectId"></param>
       /// <returns></returns>
        public Respuesta<List<TFlujo>> CompletarAdministracionFlujos(List<TFlujo> flujos, List<TFlujo> flujosOriginales, string subjectId);

       
     
    }
}
