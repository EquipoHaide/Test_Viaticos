using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Modelos;
using Entidades = Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;
using Dominio.Viaticos.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<Entidades.FlujoViaticos, Entidades.PasoViatico, ConsultaConfiguracionFlujo>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }


        public override Respuesta<ConsultaPaginada<List<FlujoViaticos>>> ConsultarFlujosDeAutorizacion(ConsultaConfiguracionFlujo parametros, string subjectId)
        {

            return new Respuesta<ConsultaPaginada<List<FlujoViaticos>>>(""); 
        }

        public override bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {
            return false;
        }

        public override bool ExisteNivelRepetido(int idTipoEntePublico, string nivel)
        {
            return false;
        }


        public override FlujoViaticos ObtenerFlujos(FlujoViaticos flujo, string subjectId)
        {

            return flujo;
        }



        //public override IEnumerable<FlujoViaticos> ObtenerFlujos(IEnumerable<FlujoViaticos> flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public override FlujoViaticos ObtenerFlujos(FlujoViaticos flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void RemoverFlujo(FlujoViaticos flujo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS