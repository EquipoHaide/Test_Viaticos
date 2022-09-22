using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso,TQuery>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class, IConsultaFlujo
    {

        public Respuesta<TFlujo> CreacionFlujo(TFlujo flujo,string subjectId);

        public Respuesta<TFlujo> ModificarFlujo(TFlujo flujo,string subjectId);

        public Respuesta<TFlujo> EliminarFlujo(TFlujo flujo,string subjectId);

        public Respuesta<ConsultaPaginada<List<TFlujo>>> Consultar(TQuery query, string subjectId);

        public Respuesta Crear(TFlujo flujo,  string subjectId);

        public Respuesta Modificar(TFlujo flujo, string subjectId);

        public Respuesta Eliminar(TFlujo flujo, string subjectId);
        


        }
}
