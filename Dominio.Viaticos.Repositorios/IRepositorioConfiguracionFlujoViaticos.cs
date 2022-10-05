using Dominio.Nucleo.Repositorios;
using Dominio.Viaticos.Modelos;
using Entidades = Dominio.Viaticos.Entidades;
using MicroServices.Platform.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Viaticos.Repositorios
{
   
    public interface IRepositorioConfiguracionFlujoViaticos : IRepository<Entidades.FlujoViatico>, IRepositorioConfiguracionFlujo<Entidades.FlujoViatico,ConsultaConfiguracionFlujo>

    {
        /// <summary>
        /// Metodo que cuenta los registros totales que tiene un Ente Publico 
        /// </summary>
        List<Entidades.FlujoViatico> ObtenerTotalFlujos(int idEntePublico);
    }

    
}
