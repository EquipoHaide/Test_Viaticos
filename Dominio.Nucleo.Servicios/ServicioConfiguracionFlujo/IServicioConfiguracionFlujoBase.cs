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
    
        public Respuesta<List<TFlujo>> AdministrarFlujos(List<TFlujo> flujos, List<TFlujo> flujoOriginal, bool existeEntePublico, string subjectId);

        public Respuesta<bool> ValidarTipoEnte(List<int> idsTipoEnte);

        public Respuesta<TFlujo> ObtenerCofiguracionFlujo(TFlujo flujo);

        public Respuesta<List<TFlujo>> GestionConfiguracionFlujos(List<TFlujo> flujos,string subjectId);

    }
}
