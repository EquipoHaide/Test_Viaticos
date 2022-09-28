using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, Dominio.Nucleo.Entidades.IPaso
    {
    
        public Respuesta<TFlujo> Crear(TFlujo flujos, bool esPredeterminado, bool esNivelRepetido, bool esEntePublico, string subjectId);

        public Respuesta<TFlujo> Modificar(TFlujo flujos, TFlujo flujoOriginal, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        public Respuesta<TFlujo> Eliminar(TFlujo flujos,string subjectId);


    }
}
