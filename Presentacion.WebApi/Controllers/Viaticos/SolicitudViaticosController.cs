using System;
using Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Modelos;
using Presentacion.WebApi.FlujoAutorizacion;

namespace Presentacion.WebApi.Controllers.Viaticos
{
    
    public class SolicitudViaticosController : FlujoAutorizacionController<Autorizacion, ConsultaSolicitudes>
    {

        public SolicitudViaticosController(Aplicacion.Nucleo.IAplicacion app)
        {
            this.App = app;
        }

    }
}
