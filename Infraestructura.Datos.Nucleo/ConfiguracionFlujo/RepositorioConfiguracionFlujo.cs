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
    public abstract class RepositorioConfiguracionFlujo<TFlujo,TPaso> : Repository<TFlujo>, IRepositorioConfiguracionFlujo<TFlujo,TPaso>
         where TFlujo : FlujoBase<TPaso>
        where TPaso : class, IPaso
    {
        public RepositorioConfiguracionFlujo(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        //public abstract void AddFlujo(TEntidad flujo);

        public abstract Respuesta<ConsultaPaginada<Dominio.Nucleo.IConsulta>> ConsultarFlujosDeAutorizacion(Dominio.Nucleo.IConsulta parametros, string subjectId);

        public abstract bool ExisteFlujoPredeterminado(int idTipoEntePublico);

        public abstract bool ExisteNivelRepetido(int idTipoEntePublico, string nivel);

        public abstract IEnumerable<TFlujo> ObtenerFlujos(IEnumerable<TFlujo> flujo, string subjectId);

        public abstract TFlujo ObtenerFlujos(TFlujo flujo, string subjectId);

        public abstract void RemoverFlujo(TFlujo flujo);

        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
