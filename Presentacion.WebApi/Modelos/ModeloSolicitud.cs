using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloSolicitud<TSolicitudCondensada, TFlujo, TPaso>
        where TFlujo : class,IFlujo<TPaso>
        where TPaso : class, IPaso
        where TSolicitudCondensada : class,IInstanciaCondensada
    {

        public int Accion { get; set; }

        public List<TSolicitudCondensada> Solicitudes { get; set; }

        public List<TFlujo> Flujos { get; set; }

    }
}
