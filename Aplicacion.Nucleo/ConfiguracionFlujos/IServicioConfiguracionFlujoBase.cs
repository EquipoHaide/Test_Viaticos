using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;


namespace Aplicacion.Nucleo
{
    public interface IServicioConfiguracionFlujoBase
    {
        Respuesta<ConsultaPaginada<Dominio.Nucleo.IConsulta>> Consultar(Dominio.Nucleo.IConsulta parametros, string subjectId);

        Respuesta<List<Dominio.Nucleo.IFlujo>> Crear(List<Dominio.Nucleo.IFlujo> flujos, string subjectId);

        void Eliminar(Dominio.Nucleo.IFlujo flujo, string subjectId);


        void Modificar(Dominio.Nucleo.IFlujo flujo, string subjectId);
    }
}
