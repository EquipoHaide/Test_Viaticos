using System;
using Dominio.Nucleo;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Viaticos.Repositorios;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;

namespace Infraestructura.Datos.Viaticos
{
    public class ViaticosMapperServices : MapperServices
    {
        public override void Maping(IAplicacion app)
        {
            app.Register<IViaticosUnidadDeTrabajo, ViaticosUnidadDeTrabajo>();
            app.Register<IRepositorioConfiguracionFlujoViaticos, RepositorioConfiguracionFlujoViaticos>();
            app.Register<IRepositorioPaso, RepositorioPaso>();


        }
    }
}