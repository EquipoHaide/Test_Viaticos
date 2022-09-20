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
using Dominio.Nucleo.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<Dominio.Viaticos.Entidades.FlujoViaticos,PasoViatico>, IRepositorioConfiguracionFlujoViaticos
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

        public override IEnumerable<Dominio.Viaticos.Entidades.FlujoViaticos> ObtenerFlujos(IEnumerable<Dominio.Viaticos.Entidades.FlujoViaticos> flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override Dominio.Viaticos.Entidades.FlujoViaticos ObtenerFlujos(Dominio.Viaticos.Entidades.FlujoViaticos flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void RemoverFlujo(Dominio.Viaticos.Entidades.FlujoViaticos flujo)
        {
            throw new NotImplementedException();
        }
    }
}

