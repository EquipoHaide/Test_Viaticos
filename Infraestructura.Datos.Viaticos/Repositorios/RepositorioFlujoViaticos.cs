using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViaticos>, IRepositorioViaticos
    {
        public RepositorioFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public bool ExisteFlujoPredeterminado()
        {
            return false;
        }

        public bool ExisteNivelRepetido()
        {
            return false;
        }

        public IEnumerable<FlujoViaticos> ObtenerFlujos(IEnumerable<FlujoViaticos> flujo, string subjectId)
        {
            IEnumerable<FlujoViaticos> lista = null ;

            return lista;
        }
    }
}
