using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : ServicioConfiguracionFlujoBase , IServicioFlujos
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";



        public void Consultar(IConsulta parametros, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Crear(List<IFlujo> flujos, string subjectId)
        {
            var respuesta = ValidarFlujo(flujos);

            //AQUI VAN EL RESTO DE VALIDACIONES NECESARIAS QUE CONSIDEREN 


            throw new NotImplementedException();
        }

        public void Eliminar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }
    }
}
