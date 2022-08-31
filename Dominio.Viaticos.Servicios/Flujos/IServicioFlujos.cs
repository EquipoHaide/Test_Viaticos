using System;
using System.Collections.Generic;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Servicios
{
    public interface IServicioFlujos
    {
        void Consultar(IConsulta parametros, string subjectId);

        void Crear(List<IFlujo> flujos, string subjectId);

        void Eliminar(IFlujo flujo, string subjectId);


        void Modificar(IFlujo flujo, string subjectId);
    }
}
