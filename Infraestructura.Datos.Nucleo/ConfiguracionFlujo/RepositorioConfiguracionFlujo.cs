using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using MicroServices.Platform.Repository;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.Datos.Nucleo
{
    public abstract class RepositorioConfiguracionFlujo<TEntidad> : Repository<TEntidad>, IRepositorioConfiguracionFlujo<TEntidad>
        where TEntidad : FlujoBase
        //where TFlujo : Flujo
    {
        public RepositorioConfiguracionFlujo(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public abstract bool ExisteFlujoPredeterminado(int idTipoEntePublico);


        public abstract bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);


        public abstract IEnumerable<TEntidad> ObtenerFlujos(IEnumerable<TEntidad> flujo, string subjectId);
        

     
        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
