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

            //var ff = (List<FlujoBase>)flujos;
            //creamos el maperr para que transforme el modelo particular de viaticos a un FlujoBase
            //var flujosComponente = MainMapper.Instance.Mapper.Map<IFlujoNew>(flujos[0]);
            // Model2 model = this.Mapper.Map<Model2>(new Model1());
            //FlujoTemp flujosComponente = MainMapper.Instance.Mapper.Map<FlujoTemp>(flujos[0]);


            var entePublico = new TipoEntePublico() {
                Descripcion = "djsfhjshfj"
            };

            var ente = MainMapper.Instance.Mapper.Map<Dominio.Nucleo.TipoEntePublico>(entePublico);

            var respuesta = this.Crear(null, "");

            
            Console.WriteLine("VALIDACION DE REGLAS PARTICULARS DE VIATICOS");

            Console.WriteLine("INVOCACION DE REPOSITORIO PARA EL GUARDADO DE LA CONFIGURACION");
        }

    }
}
