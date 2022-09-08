using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Transversal.Plataforma;
using System.Linq;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TPaso> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioDominio;
        //Dominio.IRepositorioFlujos repositorio;
 

        public abstract bool ValidarPasos(List<IFlujo<TPaso>> flujos);

        public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos)
        {

            if (flujos == null || !flujos.Any()) return new Respuesta<bool>("La lista de flujos no contiene elementos.", TAG);



            try {

                if (this.ValidarPasos(flujos))
                {
                    //Todo: Aqui todo lo que sigue

                    var respuesta = ServicioDominio.Crear(flujos);


                    //Repositorio.Guardar();


                }
            }
            catch(Exception e) {

            }
        

            return new Respuesta<bool>(true);

        }

       
    }
}
