using System;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public class ServicioPasoBas<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {
        public Respuesta<TPaso> Eliminar(TPaso paso, string subjectId)
        {
            return new Respuesta<TPaso>(paso);
        }

        public Respuesta<TPaso> Modificar(TPaso paso, TPaso flujoOriginal, string subjectId)
        {
            return new Respuesta<TPaso>(paso);
        }
    }
}
