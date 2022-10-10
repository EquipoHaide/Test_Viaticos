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
    
        //public Respuesta<List<TFlujo>> Crear(List<TFlujo> flujos, bool esPredeterminado, bool esEntePublico, string subjectId);

        //public Respuesta<List<TFlujo>> Modificar(List<TFlujo> flujos, List<TFlujo> flujoOriginal, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        //public Respuesta<TFlujo> Eliminar(TFlujo flujos, string subjectId);


        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, List<TFlujo> flujoOriginal, bool existeFlujoPredeterminado, bool existeEntePublico, string subjectId);


    }
}
