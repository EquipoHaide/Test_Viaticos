using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioAutorizacion
{
    public class ServicioAutorizacionBase<TAutorizacion, TQuery> : IServicioAutorizacionBase<TAutorizacion, TQuery>
        where TAutorizacion : class, IAutorizacion
        where TQuery : class, IConsultaSolicitud
    {

        const string TAG = "Aplicacion.Nucleo.ServicioFlujoAutorizacion";

        public virtual Dominio.Nucleo.Servicios.ServicioAutorizacion.IServicioAutorizacionBase<TAutorizacion> ServicioDominio { get; }


        public virtual IRepositorioAutorizacion<TAutorizacion, TQuery> Repositorio { get; }


        public Respuesta<ConsultaPaginada<TAutorizacion>> Consultar(TQuery parametros, string subjectId)
        {
            if (parametros == null) return new Respuesta<ConsultaPaginada<TAutorizacion>>("El modelo de consulta para obtener no es valido.", TAG);

            var recursos = Repositorio.Try(r => r.ConsultarAutorizaciones(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<TAutorizacion>>(TAG);

            return new Respuesta<ConsultaPaginada<TAutorizacion>>(recursos.Contenido);
        }

        public Respuesta AdministrarAutorizaciones(List<TAutorizacion> autorizacones, string subjectId)
        {

            var respuesta = ServicioDominio.AdministrarAutorizacion(autorizacones, subjectId);

            throw new NotImplementedException();
        }

      
    }
}
