using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;


namespace Dominio.Viaticos.Servicios
{
    public interface IServicioFlujos
    {
        void Consultar(ConsultaConfiguracionFlujo parametros, string subjectId);

        void Crear(List<Flujo> flujos, string subjectId);

        void Eliminar(Flujo flujo, string subjectId);

        void Modificar(Flujo flujo, string subjectId);
    }
}
