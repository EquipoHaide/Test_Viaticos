using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso,TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class, IConsultaFlujo
    {

        public Respuesta<List<TFlujo>> CreacionFlujo(List<TFlujo> flujos, string subjectId);

        public Respuesta<List<TFlujo>> ModificarFlujo(List<TFlujo> flujo, List<TFlujo> flujosOriginal, string subjectId);

        public Respuesta<List<TFlujo>> EliminarFlujo(List<TFlujo> flujos, string subjectId);

        public Respuesta<ConsultaPaginada<TFlujo>> Consultar(TQuery query, string subjectId);

        public Respuesta Crear(List<TFlujo> flujos,  string subjectId);

        public Respuesta Modificar(List<TFlujo> flujos, string subjectId);

        public Respuesta Eliminar(List<int> id, string subjectId);
        


        }
}
