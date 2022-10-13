using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;

namespace Presentacion.WebApi.Modelos
{
    public class ModeloSolicitud<TSolicitudCondensada>
        where TSolicitudCondensada : class,IInstanciaCondensada
    {

        public int Accion { get; set; }
        public List<TSolicitudCondensada> Solicitudes { get; set; }

    }
}
