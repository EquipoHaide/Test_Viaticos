using System;
using System.Collections.Generic;
using MicroServices.Platform.Repository.Core;

namespace Dominio.Nucleo.Repositorios.ConfiguracionFlujo
{
    public interface IRepositorioPasoBase<TPaso> : IRepository<TPaso>
        where TPaso : class,IEntity
    {

        public List<TPaso> ObtenerPasos(int idFlujo);

        /// <summary>
        /// Obtiene los pasos que se reodenaran por la eliminacion de otro
        /// </summary>
        List<TPaso> ObtenerPasosReordenar(int idPasoEliminado, int idFlujo);

    }
}
