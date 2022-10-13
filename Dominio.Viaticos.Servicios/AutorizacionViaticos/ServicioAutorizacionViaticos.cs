using System;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.SolicitudCondensada,Entidades.Autorizacion>,IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion> 
    {
        private new const string TAG = "Dominio.Viaticos.Servicios.AutorizacionViaticos";


    }
}
