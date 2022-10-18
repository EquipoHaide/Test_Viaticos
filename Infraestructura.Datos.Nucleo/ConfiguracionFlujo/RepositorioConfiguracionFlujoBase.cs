using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Datos.Nucleo
{
    public abstract class RepositorioConfiguracionFlujoBase<TFlujo,TQuery> : Repository<TFlujo>, IRepositorioConfiguracionFlujoBase<TFlujo,TQuery>
         where TFlujo : class, IEntity
        where TQuery : class, IQuery

    {
        public RepositorioConfiguracionFlujoBase(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract ConsultaPaginada<TFlujo> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

        public abstract bool ExisteFlujoPredeterminado(int idTipoEnte);

        public abstract bool ExisteNivelRepetido(int idTipoEnte);

        public abstract bool ExisteRegistroEntePublico(TFlujo flujo);

        public abstract TFlujo ObtenerConfiguracionFlujo(int idFlujo);

        public abstract List<TFlujo> ObtenerFlujosPorEntePublico(int idTipoEnte);


        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
