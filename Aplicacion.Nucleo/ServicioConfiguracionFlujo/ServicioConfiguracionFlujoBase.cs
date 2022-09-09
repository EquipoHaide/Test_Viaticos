using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

using Infraestructura.Datos.Nucleo;
using System.Linq;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Entidades;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioDominio { get; }



        /// <summary>
        /// PENDIENTE :
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        public abstract bool ValidarPasos(IFlujo<TPaso> flujos); 


        public Respuesta<IFlujo<TPaso>> Crear(IFlujo<TPaso> flujo, RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> repositorioConfiguracion)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta<IFlujo<TPaso>>("Es requerido un flujo de autorizacion ", TAG);

            if (!flujo.IsValid())
                return new Respuesta<IFlujo<TPaso>>("El Flujo es invalido",TAG);



            if (flujo.TipoFlujo.ToString() == null)
                return new Respuesta<IFlujo<TPaso>>("El tipo de flujo es requerido", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta<IFlujo<TPaso>>("La lista de pasos es requerida.", TAG);

            //CONSULTA AL REPOSITORIO
            //BUSCAR SI YA EXISTE UN FLUJO PREDETERMINADO 
            //            
            var esPredertiminado = false;
            var respuesta = ServicioDominio.Crear(flujo, esPredertiminado);

            if (respuesta.EsExito)
                {
                    if (this.ValidarPasos(flujo))
                    {
                    //Todo: Aqui todo lo que sigue 




                    //Repositorio.Guardar();

                    repositorioConfiguracion.Save();
                    }
                }

                return new Respuesta<IFlujo<TPaso>>(respuesta.Mensaje,TAG);

        }



       

       

     
    }
}
