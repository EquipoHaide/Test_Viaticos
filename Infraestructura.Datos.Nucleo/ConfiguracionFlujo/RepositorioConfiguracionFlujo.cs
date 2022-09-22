using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Datos.Nucleo
{
    public abstract class RepositorioConfiguracionFlujo<TFlujo,TPaso,TQuery> : Repository<TFlujo>, IRepositorioConfiguracionFlujo<TFlujo,TPaso,TQuery>
         where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
        where TQuery : class, IConsultaFlujo

    {
        public RepositorioConfiguracionFlujo(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract Respuesta<ConsultaPaginada<List<TFlujo>>> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

        public abstract bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        public abstract bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);

        public abstract TFlujo ObtenerFlujos(TFlujo flujo, string subjectId);

        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
