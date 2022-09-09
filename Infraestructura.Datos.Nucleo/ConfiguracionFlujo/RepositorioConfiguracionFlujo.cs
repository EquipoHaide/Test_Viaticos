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
        //Agregar los metodos particulares que se requieren para realizar el guardado general
    }
}
