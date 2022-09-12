using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViaticos>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {
            return true;
        }

        public bool ExisteNivelRepetido(int idTipoEntePublico, string nivel)
        {
            return false;
        }

        public IEnumerable<FlujoViaticos> ObtenerFlujos(IEnumerable<FlujoViaticos> flujo, string subjectId)
        {
            IEnumerable<FlujoViaticos> lista = null;
            return lista;
        }
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS