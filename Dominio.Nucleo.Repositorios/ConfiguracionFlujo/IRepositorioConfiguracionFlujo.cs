using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRecurso"></typeparam>
    public interface IRepositorioConfiguracionFlujo<TFlujoEntidad, TQuery> : IRepository<TFlujoEntidad>
        where TFlujoEntidad : class, IEntity
        where TQuery : class, IConsultaFlujo
    {
        //Agregar los metodos particulares que se requieren para realizar el guardado general

        bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);
       
        List<TFlujoEntidad> ObtenerFlujos(int idEntePublico);

        public ConsultaPaginada<TFlujoEntidad> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

        //void RemoverFlujo(TEntidad flujo);

        //void AddFlujo(TEntidad flujo);
    }
}