using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Servicios;
using Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioFlujos : Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.ServicioConfiguracionFlujoBase<Dominio.Viaticos.Modelos.PasoViatico, Dominio.Viaticos.Entidades.FlujoViaticos>, IServicioFlujos<Dominio.Viaticos.Modelos.PasoViatico>
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioFlujos";

        public void Consultar(ConsultaConfiguracionFlujo parametros, string subjectId)
        {
          
            throw new NotImplementedException();
        }

        public void Crear(List<Flujo> flujos, string subjectId)
        {
            //flujo.Pasos.Cast<IPaso>().ToList()
            //var flujo = flujos.Cast<Dominio.Nucleo.IFlujo>().ToList();
           // var respuesta = ValidarFlujo(flujos.Cast<Dominio.Nucleo.IFlujo>().ToList());

            throw new NotImplementedException();
        }

        public void Eliminar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override Entidades.FlujoViaticos ObtenerEntidad(IFlujo<PasoViatico> flujo)
        {

            var modeloFlujo = MainMapper.Instance.Mapper.Map<Dominio.Viaticos.Modelos.FlujoViaticos>(flujo);

            var entidad = modeloFlujo.ToEntity<Dominio.Viaticos.Entidades.FlujoViaticos>();

            var entidadNueva = new Entidades.FlujoViaticos
            {
                IdNivelEmpleado = flujo.NivelEmpleado.Id,
                IdTipoEntePublico = flujo.TipoEntePublico.Id,
                TipoFlujo = flujo.TipoFlujo
            };

            return entidadNueva;
        }

    }
}
