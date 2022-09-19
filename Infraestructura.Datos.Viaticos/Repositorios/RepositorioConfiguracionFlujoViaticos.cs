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
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase<PasoViatico>,PasoViatico>, IRepositorioConfiguracionFlujoViaticos
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

        public override IEnumerable<FlujoBase<PasoViatico>> ObtenerFlujos(IEnumerable<FlujoBase<PasoViatico>> flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override FlujoBase<PasoViatico> ObtenerFlujos(FlujoBase<PasoViatico> flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void RemoverFlujo(FlujoBase<PasoViatico> flujo)
        {
            throw new NotImplementedException();
        }
    }
}

