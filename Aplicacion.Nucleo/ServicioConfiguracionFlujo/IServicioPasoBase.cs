using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioPasoBase<TPaso>
    where TPaso : class, IPaso
    {
        //public Respuesta<TPaso> ModificarPaso(TPaso paso, string subjectId);

        //public Respuesta<TPaso> EliminarPaso(TPaso paso, string subjectId);

        public Respuesta Modificar(TPaso paso, string subjectId);

        public Respuesta Eliminar(int paso, string subjectId);

    }
}
