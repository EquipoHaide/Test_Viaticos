using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public class ServicioConfiguracionPasoBase<TPaso> : IServicioConfiguracionPasoBase<TPaso>
        where TPaso : class, IPaso
    {
        public Respuesta<List<TPaso>> Consultar()
        {
            throw new NotImplementedException();
        }

        public Respuesta Crear(TPaso flujo, string subjectId)
        {
            return new Respuesta();
        }

        public Respuesta Eliminar(TPaso flujo, string subjectId)
        {
            return new Respuesta();
        }

        public Respuesta Modificar(TPaso flujo, string subjectId)
        {
            return new Respuesta();
        }
    }
}
