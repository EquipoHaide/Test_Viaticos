﻿using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso,TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class, IQuery
    {

        public Respuesta<ConsultaPaginada<TFlujo>> Consultar(TQuery query, string subjectId);

        public Respuesta<ResumenInformacion> AdministrarFlujos(List<TFlujo> flujos, string subjectId);

        public Respuesta<TFlujo> ObtenerConfiguracionFlujo(int idFlujo);

    }
}
