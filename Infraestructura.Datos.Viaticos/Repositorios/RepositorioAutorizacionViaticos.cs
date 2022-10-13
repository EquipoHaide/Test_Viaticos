using System;
using Entidad = Dominio.Viaticos.Entidades;
using Modelo = Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Nucleo.AutorizacionSolicitud;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;
using Dominio.Viaticos.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioAutorizacionViaticos : RepositorioAutorizacionBase<Entidad.SolicitudCondensada, Entidad.Autorizacion, Modelo.ConsultaSolicitudes>, IRepositorioAutorizacionViaticos
    {
        
        public RepositorioAutorizacionViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override ConsultaPaginada<Entidad.SolicitudCondensada> ConsultarAutorizaciones(Modelo.ConsultaSolicitudes parametros, string subjectId)
        {

            return new ConsultaPaginada<Entidad.SolicitudCondensada>();
        }

        public override List<Autorizacion> ObtenerAutorizacion(List<int> IdsAutorizacion)
        {
            throw new NotImplementedException();
        }
    }
}
