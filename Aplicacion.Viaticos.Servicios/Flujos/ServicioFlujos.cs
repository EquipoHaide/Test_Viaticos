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

        public override Respuesta<List<Flujo>> Crear(List<Flujo> flujos, string subjectId)
        {

            var respuesta = this.ValidarFlujo(flujos);
            if(respuesta.EsError)
                return new Respuesta<List<Flujo>>("","");

             


            return new Respuesta<List<Flujo>>("");
        }

        public override void Eliminar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public override void Modificar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        public void MetodoExtra()
        {
            throw new NotImplementedException();
        }

    }
}
