using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;

using ServiciosDominio = Dominio.Viaticos.Servicios;

namespace Aplicacion.Viaticos.Servicios
{
    public class ServicioFlujosNew: ServicioConfiguracionFlujoBaseNew , IServicioFlujosNew
    {

        const string TAG = "Aplicacion.Seguridad.Servicios.Grupos.ServicioGrupos";

        Nucleo.IAplicacion App { get; set; }

        


        public ServicioFlujosNew(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<ConsultaPaginada<IFlujoNew>> Consultar(IConsulta parametros, string subjectId)
        {
            Console.WriteLine("LLEGAMOS A LA CONSULTA");

            //De momento lo dejare asi
            return new Respuesta<ConsultaPaginada<IFlujoNew>>("");
        }
    }
}
