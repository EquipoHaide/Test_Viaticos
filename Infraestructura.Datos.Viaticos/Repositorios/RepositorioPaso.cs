using System;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Nucleo.ConfiguracionFlujo;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioPaso : RepositorioPasoBase<Dominio.Viaticos.Entidades.PasoViatico>, IRepositorioPaso
    {
        public RepositorioPaso(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

    }
}
