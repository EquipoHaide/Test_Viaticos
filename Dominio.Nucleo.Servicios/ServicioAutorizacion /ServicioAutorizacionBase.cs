using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TAutorizacion> : IServicioAutorizacionBase<TAutorizacion>
        where TAutorizacion : class, IAutorizacion
    {
        public Respuesta<List<TAutorizacion>> AdministrarAutorizacion(List<TAutorizacion> autorizaciones, string subjectId)
        {
            return new Respuesta<List<TAutorizacion>>("");
        }
    }
}
