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
    public interface IRepositorioConfiguracionFlujo<TFlujoEntidad, TPasoEntidad> : IRepository<TFlujoEntidad>
        where TFlujoEntidad : class, IFlujo<TPasoEntidad>
        where TPasoEntidad : class, IPaso
    {
        //Agregar los metodos particulares que se requieren para realizar el guardado general

        bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);
       
        TFlujoEntidad ObtenerFlujos(TFlujoEntidad flujo, string subjectId);

        public Respuesta<ConsultaPaginada<IConsulta>> ConsultarFlujosDeAutorizacion(IConsulta parametros, string subjectId);

        //void RemoverFlujo(TEntidad flujo);

        //void AddFlujo(TEntidad flujo);
    }
}