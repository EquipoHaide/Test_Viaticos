using System;
using System.Collections.Generic;
using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Nucleo.Servicios;
using Dominio.Viaticos.Modelos;
using Infraestructura.Transversal.Plataforma;
using Paso = Dominio.Viaticos.Modelos.Paso;
using ServiciosDominio = Dominio.Viaticos.Servicios;

namespace Aplicacion.Viaticos.Servicios
{
    public class ServicioFlujos: IServicioFlujos
    {

        const string TAG = "Aplicacion.Seguridad.Servicios.Grupos.ServicioGrupos";

        Nucleo.IAplicacion App { get; set; }

        ServiciosDominio.IServicioFlujos servicio;
        ServiciosDominio.IServicioFlujos Servicio => App.Inject(ref servicio);
        //public override IServicioConfiguracionFlujoBase<Flujo, Paso> ServicioDominio => this.Servicio;


        public ServicioFlujos(Nucleo.IAplicacion app)
        {
            App = app;
        }

        public Respuesta<ConsultaPaginada<ConsultaConfiguracionFlujo>> Consultar(ConsultaConfiguracionFlujo parametros, string subjectId)
        {

          
            throw new NotImplementedException();
        }

        public Respuesta<List<Flujo>> Crear(List<Flujo> flujos, string subjectId)
        {

            foreach (var item in flujos)
            {
                if (item.IsValid())
                {
                    return new Respuesta<List<Flujo>>("", "");
                }
            }

            var respuesta = Servicio.ValidarFlujo(flujos);
            if(respuesta.EsError)
                return new Respuesta<List<Flujo>>(respuesta.Mensaje,TAG);
            ///
            ///OTRAS VALIDACIONES DE LOS PROGRAMADORES
            ///


             

            return new Respuesta<List<Flujo>>("");
        }

        public void Eliminar(Flujo flujo, string subjectId)
        {
            throw new NotImplementedException();
        }

        public void Modificar(Flujo flujo, string subjectId)
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
