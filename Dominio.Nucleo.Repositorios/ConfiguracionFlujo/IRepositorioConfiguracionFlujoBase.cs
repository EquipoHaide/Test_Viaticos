using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.FlujoAutorizacion;
using Infraestructura.Transversal.Plataforma;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRecurso"></typeparam>
    public interface IRepositorioConfiguracionFlujoBase<TFlujo, TQuery> : IRepository<TFlujo>
        where TFlujo : class, IEntity
        where TQuery : class, IQuery
    {
        //Agregar los metodos particulares que se requieren para realizar el guardado general

        //bool ExisteFlujoPredeterminado(int idTipoEnte);

        //bool ExisteNivelRepetido(int idTipoEnte);

        /// <summary>
        /// Valida que no existe flujo de autorizacion ya registrados con dicho Ente Publico 
        /// </summary>
      
        bool ExisteRegistroEntePublico(TFlujo flujo);

        List<TFlujo> ObtenerFlujosPorEntePublico(int idTipoEnte);
        //List<TFlujo> ObtenerFlujosPorAutorizacion(List<int> idsAutorizacion);

        ConsultaPaginada<TFlujo> ConsultarFlujosDeAutorizacion(TQuery parametros, string subjectId);

       

    }
}