using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViaticos>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public ConsultaPaginada<IConsulta> ConsultarFlujosDeAutorizacion(IConsulta parametros, string subjectId)
        {

             
            return new ConsultaPaginada<IConsulta>();
        }

        public bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {
            return true;
        }

        public bool ExisteNivelRepetido(int idTipoEntePublico, string nivel)
        {
            return false;
        }

        public FlujoViaticos ObtenerFlujos(FlujoViaticos flujo, string subjectId)
        {
            FlujoViaticos lista = null;
            return lista;
        }
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS