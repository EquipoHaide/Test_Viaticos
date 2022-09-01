using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;

using DominioFlujo = Dominio.Viaticos.Servicios;

namespace Aplicacion.Viaticos.Servicios
{
    public class ServicioFlujos: ServicioConfiguracionFlujoBase<Flujo, ConsultaConfiguracionFlujo, Paso> , IServicioFlujos
    {

        const string TAG = "Aplicacion.Seguridad.Servicios.Grupos.ServicioGrupos";

        Nucleo.IAplicacion App { get; set; }
        DominioFlujo.IServicioFlujos servicio;
        DominioFlujo.IServicioFlujos Servicio => App.Inject(ref servicio);


        public ServicioFlujos(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public override Respuesta<ConsultaPaginada<ConsultaConfiguracionFlujo>> Consultar(ConsultaConfiguracionFlujo parametros, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override Respuesta<List<Flujo>> Crear(List<IFlujo> flujos, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void Eliminar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void Modificar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }





        //public override Respuesta<ConsultaPaginada<Dominio.Nucleo.IConsulta>> Consultar(Dominio.Nucleo.IConsulta parametros, string subjectId)
        //{
        //    //Servicio.Consultar(parametros, subjectId);
        //    throw new NotImplementedException();
        //}

        //public override Respuesta<List<Dominio.Nucleo.IFlujo>> Crear(List<Dominio.Nucleo.IFlujo> flujos, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Eliminar(Dominio.Nucleo.IFlujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Modificar(Dominio.Nucleo.IFlujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}


        //public Respuesta<ConsultaPaginada<IConsulta>> Consultar(IConsulta parametros, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Respuesta<List<IFlujo>> Crear(List<IFlujo> flujos, string subjectId)
        //{
        //    Servicio.Crear(flujos, subjectId);
        //    throw new NotImplementedException();
        //}

        //public void Eliminar(IFlujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Modificar(IFlujo flujo, string subjectId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
