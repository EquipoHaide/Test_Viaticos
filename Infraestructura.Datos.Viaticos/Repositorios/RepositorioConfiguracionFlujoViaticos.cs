using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<FlujoViaticos, PasoViatico>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override Respuesta<ConsultaPaginada<IConsulta>> ConsultarFlujosDeAutorizacion(IConsulta parametros, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {
            throw new NotImplementedException();
        }

        public override bool ExisteNivelRepetido(int idTipoEntePublico, string nivel)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<FlujoViaticos> ObtenerFlujos(IEnumerable<FlujoViaticos> flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override FlujoViaticos ObtenerFlujos(FlujoViaticos flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void RemoverFlujo(FlujoViaticos flujo)
        {
            throw new NotImplementedException();
        }
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS