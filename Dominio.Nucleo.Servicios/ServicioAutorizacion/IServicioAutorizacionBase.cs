﻿using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public interface IServicioAutorizacionBase<TInstanciaCondensada,TAutorizacion>
        where TAutorizacion : class, IAutorizacion
        where TInstanciaCondensada : class, IInstanciaCondensada
    {

        public Respuesta<List<TInstanciaCondensada>> AdministrarAutorizacion(List<TInstanciaCondensada> autorizaciones, string subjectId);
    }
}
