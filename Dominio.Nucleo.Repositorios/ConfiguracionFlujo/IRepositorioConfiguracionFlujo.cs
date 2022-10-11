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

        //bool ExisteFlujoPredeterminado(int idTipoEnte);

        //bool ExisteNivelRepetido(int idTipoEnte);

        /// <summary>
        /// Valida que no existe flujo de autorizacion ya registrados con dicho Ente Publico 
        /// </summary>
      
        bool ExisteRegistroEntePublico(TFlujoEntidad flujo);

        List<TFlujoEntidad> ObtenerFlujosPorEntePublico(int idTipoEnte);

        ConsultaPaginada<TFlujoEntidad> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

       

    }
}