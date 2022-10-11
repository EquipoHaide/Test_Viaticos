using System;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.Autorizacion>,IServicioAutorizacionViaticos<Entidades.Autorizacion> 
    {
        private new const string TAG = "Dominio.Viaticos.Servicios.AutorizacionViaticos";


    }
}
