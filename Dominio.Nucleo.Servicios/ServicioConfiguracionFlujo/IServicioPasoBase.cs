using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioPasoBase<TPaso>
        where TPaso : class,IPaso
    {
        public Respuesta<TPaso> Modificar(TPaso paso, TPaso pasoOriginal, string subjectId);

        public Respuesta<TPaso> Eliminar(TPaso paso, string subjectId);

        public Respuesta ReordenarPosicionesPorEliminacion(List<TPaso> listaPaso, string subjectId);


    }
}
