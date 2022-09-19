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
    public interface IRepositorioConfiguracionFlujo<TFlujo,TPaso> : IRepository<TFlujo>
        where TFlujo : FlujoBase<TPaso>
        where TPaso : class, IPaso
    {
        //Agregar los metodos particulares que se requieren para realizar el guardado general

        bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);
       
        TFlujo ObtenerFlujos(TFlujo flujo, string subjectId);

        public Respuesta<ConsultaPaginada<IConsulta>> ConsultarFlujosDeAutorizacion(IConsulta parametros, string subjectId);
  
    }
}