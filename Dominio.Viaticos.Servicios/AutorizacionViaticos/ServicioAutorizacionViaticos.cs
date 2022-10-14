using System;
using Dominio.Nucleo.Servicios.ServicioAutorizacion;

namespace Dominio.Viaticos.Servicios
{
    public class ServicioAutorizacionViaticos : ServicioAutorizacionBase<Entidades.SolicitudCondensada,Entidades.Autorizacion, Entidades.FlujoViatico, Entidades.PasoViatico>,IServicioAutorizacionViaticos<Entidades.SolicitudCondensada, Entidades.Autorizacion,Entidades.FlujoViatico,Entidades.PasoViatico> 
    {
        private new const string TAG = "Dominio.Viaticos.Servicios.AutorizacionViaticos";


    }
}
