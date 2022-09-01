using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Modelos;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase , IServicioFlujos
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public void Consultar(ConsultaConfiguracionFlujo parametros, string subjectId)
        {
          
            throw new NotImplementedException();
        }

        public void Crear(List<Modelos.Flujo> flujos, string subjectId)
        {
            //flujo.Pasos.Cast<IPaso>().ToList()
            //var flujo = flujos.Cast<Dominio.Nucleo.IFlujo>().ToList();
            var respuesta = ValidarFlujo(flujos.Cast<Dominio.Nucleo.IFlujo>().ToList());

            throw new NotImplementedException();
        }

        public void Eliminar(Modelos.Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Modelos.Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }




        //public void Consultar(Modelos.ConsultaConfiguracionFlujo parametros, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Crear(List<Flujo> flujos, string subjectId)
        //{
        //    var respuesta = ValidarFlujo(flujos);

        //    //AQUI VAN EL RESTO DE VALIDACIONES NECESARIAS QUE CONSIDEREN 


        //    throw new NotImplementedException();
        //}


        //public void Eliminar(Flujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}



        //public void Modificar(Flujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
