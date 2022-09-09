using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Datos.Nucleo;
using System.Linq;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioDominio;
        //Dominio.IRepositorioFlujos repositorio;

        //public const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        /// <summary>
        /// PENDIENTE :
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        public abstract bool ValidarPasos(List<IFlujo<TPaso>> flujos); 

        public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos, RepositorioConfiguracionFlujo<Flujo> repositorioConfiguracionFlujo)
        {
                //Valida que el objeto no este vacio
                if (flujos == null || !flujos.Any())
                    return new Respuesta<bool>("Es requerido al menos un flujo de autorizacion ", TAG);
            


                foreach (var flujo in flujos)
                {
                    if (!flujo.IsValid())
                        return new Respuesta<bool>(true);

                    if (flujo.TipoEntePublico == null)
                        return new Respuesta<bool>("El tipo ente publico es requerida",TAG);

                    if (flujo.NivelEmpleado == null)
                        return new Respuesta<bool>("El nivel de empleado es requerido",TAG);

                    if (flujo.TipoFlujo.ToString() == null)
                        return new Respuesta<bool>("El tipo de flujo es requerido",TAG);

                    if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                        return new Respuesta<bool>("La lista de pasos es requerida.", TAG);

                }

                var respuesta = ServicioDominio.Crear(flujos);

                if (respuesta.EsExito)
                {
                    if (this.ValidarPasos(flujos))
                    {
                    //Todo: Aqui todo lo que sigue 




                    //Repositorio.Guardar();

                    repositorioConfiguracionFlujo.Save();
                    }
                }

            //}
            //catch(Exception e) {

            //}
        

                return new Respuesta<bool>(respuesta.Mensaje,TAG);

        }



        private Respuesta<bool> ValidarFlujo(IFlujo<TPaso> flujo)
        {
            if (flujo.TipoEntePublico == null)
                return new Respuesta<bool>("El tipo ente publico es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo.NivelEmpleado == null)
                    return new Respuesta<bool>("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            //Aplicamos la validacion con respecto a los pasos.
            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta<bool>("La lista de pasos es requerida.", TAG);


            //foreach (var item in flujo.Pasos)
            //{
            //    var respuestaPaso = ServicioDominio.Crear(item);

            //    if (!respuestaPaso.Contenido)
            //        return new Respuesta<bool>("La información de los pasos esta incompleta", TAG);
            //}

            return new Respuesta<bool>("");
        }

        public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos, IRepositorioConfiguracionFlujo<Flujo> repositorioConfiguracionFlujo)
        {
            throw new NotImplementedException();
        }
    }
}
