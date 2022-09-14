using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {

        public Respuesta<TFlujo> Crear(TFlujo flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        public Respuesta<TFlujo> Modificar(TFlujo flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        //  IEnumerable<TEntidad> ObtenerFlujos(IEnumerable<TEntidad> flujo, string subjectId);

        public Respuesta<TFlujo> Eliminar(TFlujo flujos,string subjectId);


    }
}
