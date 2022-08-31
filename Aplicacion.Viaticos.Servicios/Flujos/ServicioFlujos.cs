using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

using DominioFlujo = Dominio.Viaticos.Servicios;

namespace Aplicacion.Viaticos.Servicios.Flujos
{
    public class ServicioFlujos : IServicioFlujos
    {

        const string TAG = "Aplicacion.Seguridad.Servicios.Grupos.ServicioGrupos";

        Nucleo.IAplicacion App { get; set; }
        DominioFlujo.IServicioFlujos servicio;
        DominioFlujo.IServicioFlujos Servicio => App.Inject(ref servicio);

        public ServicioFlujos(Nucleo.IAplicacion app)
        {
            App = app;
        }

     
        public Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId)
        {
            throw new NotImplementedException();
        }

        public Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId)
        {
            Servicio.Crear(flujos, subjectId);
            throw new NotImplementedException();
        }

        public void Eliminar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(IFlujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }
    }
}
