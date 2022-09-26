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
    public abstract class RepositorioConfiguracionFlujo<TFlujo,TQuery> : Repository<TFlujo>, IRepositorioConfiguracionFlujo<TFlujo,TQuery>
         where TFlujo : class, IEntity
        where TQuery : class, IConsultaFlujo

    {
        public RepositorioConfiguracionFlujo(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract ConsultaPaginada<TFlujo> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

        public abstract bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        public abstract bool ExisteNivelRepetido(int idTipoEntePublico, int idNivel);

        public abstract TFlujo ObtenerFlujo(int id);
        

        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
