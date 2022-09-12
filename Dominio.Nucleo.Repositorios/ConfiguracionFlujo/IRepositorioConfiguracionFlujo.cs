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
    public interface IRepositorioConfiguracionFlujo<TEntidad> : IRepository<TEntidad>
        where TEntidad : Entidades.FlujoBase 
    {
        //Agregar los metodos particulares que se requieren para realizar el guardado general

        bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);
       
        IEnumerable<TEntidad> ObtenerFlujos(IEnumerable<TEntidad> flujo, string subjectId);
    }
}