using Aplicacion.Nucleo;
using Aplicacion.Viaticos.Servicios;
using Dominio.Nucleo;
using EntidadesNucleo = Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Repositorios;
using Dominio.Viaticos.Modelos;
using EntidadesViaticos = Dominio.Viaticos.Entidades;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.Modelos;
using Presentacion.WebApi.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentacion.WebApi.FlujosAutorizacion
{
    public class ConfiguracionFlujoAutorizacionBaseController<TPaso, TConsulta> : ControllerBase, IConfiguracionFlujoAutorizacionBaseController<TPaso, TConsulta>
         where TConsulta : IConsulta
         where TPaso : IPaso
    {
        public Aplicacion.Nucleo.IAplicacion App { get; set; }
 
        public virtual Aplicacion.Nucleo.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioConfiguracionFlujoBase { get; }

        IRepositorioConfiguracionFlujoViaticos repositorioConfiguracionFlujoViaticos;
        IRepositorioConfiguracionFlujoViaticos RepositorioConfiguracionFlujoViaticos => App.Inject(ref repositorioConfiguracionFlujoViaticos);
        public virtual IRepositorioConfiguracionFlujo<EntidadesViaticos.FlujoViaticos> RepositorioViaticos => this.RepositorioConfiguracionFlujoViaticos;

        IRepositorioAcciones repositorio;
        IRepositorioAcciones Repositorio => App.Inject(ref repositorio);


        public virtual IRepositorioRecurso<RecursoAccion> RepositorioRecurso => this.Repositorio;

      



        [HttpGet("recursos")]
        public object ConsultarConfiguracionFlujo([FromQuery] TConsulta filtro)
        {
            try
            {
                var consulta = ServicioConfiguracionFlujoBase.Consultar(filtro, this.GetSubjectId());

                if (consulta.EsError)
                {
                    if (consulta.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(consulta.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(consulta.Mensaje);
                }

                return this.ApiResult(new { consulta });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }


        [HttpPost("recursos")]
        public object Crear([FromBody] ModeloConfiguracionFlujo<TPaso> config)   
        {
            try {
                //PROBE LA INYECCION DE LOS RECURSOS Y LO HIZO SIN PROBLEMAS.
                //var repoRecurso = Repositorio;
                //var repoRecursoINYECTADO = RepositorioRecurso;

                //Hasta aqui llega bien la inyeccion, asi como lo hace con recursos.
                var repo = RepositorioConfiguracionFlujoViaticos;
                //Falta pasarle el repositorio especifico que usara viaticos
                var resultado = ServicioConfiguracionFlujoBase.Crear(config.Flujo, null, this.GetSubjectId()); ;
                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado});
            }
             catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }

        }


        [HttpPut("recursos")]
        public object Modificar([FromBody] ModeloConfiguracionFlujo<TPaso> config)
        {
            try
            {
                var resultado = ServicioConfiguracionFlujoBase.Modificar(config.Flujo, null, this.GetSubjectId());
                if (resultado.EsError)
                {
                    if (resultado.Estado == EstadoProceso.Fatal)
                        return this.ApiResult(resultado.ExcepcionInterna, App.GetLogger());

                    return this.ApiResult(resultado.Mensaje);
                }

                return this.ApiResult(new { resultado });
            }
            catch (Exception e)
            {
                return this.ApiResult(e, App.GetLogger());
            }
        }


        [HttpDelete("recursos")]
        public object Eliminar(ModeloConfiguracionFlujo<TPaso> config)
        {
            throw new NotImplementedException();
        }

    }
}