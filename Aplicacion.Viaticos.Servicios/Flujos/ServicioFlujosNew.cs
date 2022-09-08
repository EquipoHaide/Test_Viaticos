using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;

using ServiciosDominio = Dominio.Viaticos.Servicios;
using TipoEntePublico = Dominio.Viaticos.Modelos.TipoEntePublico;

namespace Aplicacion.Viaticos.Servicios
{
    public class ServicioFlujosNew: ServicioConfiguracionFlujoBaseNew , IServicioFlujosNew<FlujoViaticos>
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

        public void CrearViaticos(List<FlujoViaticos> flujos)
        {

            
            //convertir flujoViaticos a IFlujoNew
            //metodo de la nuestra clase base
            var respuesta = this.Crear(null, "");

            
            Console.WriteLine("VALIDACION DE REGLAS PARTICULARS DE VIATICOS");

            Console.WriteLine("INVOCACION DE REPOSITORIO PARA EL GUARDADO DE LA CONFIGURACION");
        }

    }
}
