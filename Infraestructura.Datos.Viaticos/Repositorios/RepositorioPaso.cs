using System;
using System.Collections.Generic;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Nucleo.ConfiguracionFlujo;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using System.Linq;


namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioPaso : RepositorioPasoBase<PasoViatico>, IRepositorioPaso
    {
        
        public RepositorioPaso(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override List<PasoViatico> ObtenerPasos(int idFlujo)
        {
          
            var listaPasos = (from u in Set
                              where u.IdConfiguracionFlujo == idFlujo
                              select u).ToList();

            return listaPasos;
        }
    }
}
