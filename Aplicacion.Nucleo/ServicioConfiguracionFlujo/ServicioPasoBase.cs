using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios.ConfiguracionFlujo;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioPasoBase<TPaso> : IServicioPasoBase<TPaso>
        where TPaso : class, IPaso
    {

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioPasoBase<TPaso> ServicioDominio { get; }

        public virtual IRepositorioPasoBase<TPaso> Repositorio { get; }

        // <summary>
        /// El ModificarPaso se usa para añadir las validaciones adicionales de tu paso  
        /// </summary>
        public abstract Respuesta<TPaso> ModificarPaso(TPaso paso, string subjectId);

        // <summary>
        /// El ModificarPaso se usa para añadir las validaciones adicionales de tu paso  
        /// </summary>
        public abstract Respuesta<TPaso> EliminarPaso(TPaso paso, string subjectId);


        public Respuesta Eliminar(int id, string subjectId)
        {
            TPaso paso = null;
            var respuesta = ServicioDominio.Eliminar(paso, subjectId);

            if (respuesta.EsError)
                return respuesta.ErrorBaseDatos();

            return new Respuesta();
        }

        public Respuesta Modificar(TPaso paso, string subjectId)
        {

            TPaso pasoOriginal = null;

            var respuesta = ServicioDominio.Modificar(paso, pasoOriginal, subjectId);

            if (respuesta.EsError)
                return respuesta.ErrorBaseDatos();

            return new Respuesta();
        }
    }
}
