using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

using Infraestructura.Datos.Nucleo;

using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioDominio { get; }


        public virtual IRepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> Repositorio { get; }

        /// <summary>
        /// PENDIENTE :
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        public abstract bool ValidarPasos(IFlujo<TPaso> flujos); 


        public Respuesta Crear(IFlujo<TPaso> flujo, RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> repositorioConfiguracion, string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            if (!flujo.IsValid())
                return new Respuesta("El Flujo es invalido",TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta("La lista de pasos es requerida.", TAG);

            if (flujo.TipoFlujo.ToString() == null)
                return new Respuesta("El tipo de flujo es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo.NivelEmpleado == null)
                    return new Respuesta("El nivel de empleado es requerido", TAG);

                if (flujo.NivelEmpleado.Nivel.ToString().IsNullOrEmptyOrWhiteSpace())
                    return new Respuesta("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            //CONSULTA AL REPOSITORIO
            //BUSCAR SI YA EXISTE UN FLUJO PREDETERMINADO 
            //            
            var esPredertiminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado());


            if (esPredertiminado.EsError)
            {
                return esPredertiminado.ErrorBaseDatos(TAG);
            }

            ////CONSULTA AL REPOSITORIO 
            ///BUSCA SI EXITE ALGUN FLUJO PARTICULAR CON EL MISMO NIVEL 
            ///
            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido());


            if (esNivelRepetido.EsError)
            {
                return esPredertiminado.ErrorBaseDatos(TAG);
            }

            //var respuesta = Servicio.Crear(concepto.ToEntity<Entidades.Concepto>(), existeAccionConcepto.Contenido, esConfiguracionInactivo.Contenido, subjectId);

            var respuesta = ServicioDominio.Crear(flujo, esPredertiminado.Contenido, esNivelRepetido.Contenido,subjectId);

            if (respuesta.EsExito)
            {
                if (this.ValidarPasos(flujo))
                {
                    //Todo: Aqui todo lo que sigue 
                    //---------------PENDIENTE------------------
                    Repositorio.Add(***);

                    var save = repositorioConfiguracion.Try(r => r.Save());

                    if (save.EsError)
                    {
                        //App.GetLogger().Log.Information(save.ExcepcionInterna, save.Mensaje);
                        return save.ErrorBaseDatos(TAG);
                    }

                    return new Respuesta();

                }

                return new Respuesta(respuesta.Mensaje, TAG);
            }

             return new Respuesta(respuesta.Mensaje,TAG);

        }



       

       

     
    }
}
