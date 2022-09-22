using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViatico>
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

        public IEnumerable<FlujoViatico> ObtenerFlujos(IEnumerable<FlujoViatico> flujo, string subjectId)
        {
            IEnumerable<FlujoViatico> lista = null ;

            return lista;
        }
    }
}
