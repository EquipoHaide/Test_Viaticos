using System;
using Entidad = Dominio.Viaticos.Entidades;
using Modelo = Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Nucleo.AutorizacionSolicitud;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Infraestructura.Transversal.Plataforma;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioAutorizacionViaticos : RepositorioAutorizacionBase<Entidad.Autorizacion, Modelo.ConsultaSolicitudes>, IRepositorioAutorizacionViaticos
    {
        
        public RepositorioAutorizacionViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override ConsultaPaginada<Entidad.Autorizacion> ConsultarAutorizaciones(Modelo.ConsultaSolicitudes parametros, string subjectId)
        {

            return new ConsultaPaginada<Entidad.Autorizacion>();
        }
    }
}
