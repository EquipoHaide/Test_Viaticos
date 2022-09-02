using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;


namespace Aplicacion.Nucleo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TConsulta,TPaso>
    {
        Respuesta<ConsultaPaginada<TConsulta>> Consultar(TConsulta parametros, string subjectId);

        Respuesta<List<TFlujo>> Crear(List<TFlujo> flujos, string subjectId);

        void Eliminar(TFlujo flujo, string subjectId);

        void Modificar(TFlujo flujo, string subjectId);
    }
}
