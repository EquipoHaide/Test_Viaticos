using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionPasoBase<TPaso>
    where TPaso : class, IPaso
    {
        public Respuesta<List<TPaso>> Consultar();
        public Respuesta Crear(TPaso flujo, string subjectId);
        public Respuesta Modificar(TPaso flujo, string subjectId);
        public Respuesta Eliminar(TPaso flujo, string subjectId);

    }
}
