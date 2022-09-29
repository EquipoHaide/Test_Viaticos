using System;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioPasos<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {

        Respuesta<TPaso> Eliminar(TPaso paso, bool validacionExtra, string subjectId);

        Respuesta<TPaso> Modificar(TPaso paso, TPaso pasoOriginal, bool validacionExtra, string subjectId);

    }
}
