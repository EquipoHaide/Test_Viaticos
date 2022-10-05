using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Modelos;
using Entidades = Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;
using Dominio.Viaticos.Entidades;
using System.Linq;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<Entidades.FlujoViatico,ConsultaConfiguracionFlujo>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override ConsultaPaginada<FlujoViatico> ConsultarFlujosDeAutorizacion(ConsultaConfiguracionFlujo parametros, string subjectId)
        {

            IQueryable<FlujoViatico> result = null;
           
            if (parametros.Query.IsNotNullOrEmptyOrWhiteSpace())//Busqueda Generica
            {
                var parametro = parametros.Query.Trim().ToLower();

            
                result = from u in Set
                         //where u.NombreFlujo.Trim().ToLower() == parametro
                         select new FlujoViatico()
                             {
                                 Id = u.Id,
                                 IdTipoEnte = u.IdTipoEnte,
                            
                                 Activo = u.Activo,
                                 Pasos = u.Pasos,
                                 //TipoAutorizacion = u.IdTipoEnte
                             };

            }
            else //Busqueda Avanzada
            {
                

                if (parametros.IdEntePublico > 0)
                {
                    result = from u in Set
                             where u.IdTipoEnte == parametros.IdEntePublico
                             select new FlujoViatico()
                             {
                                 Id = u.Id,
                                 IdTipoEnte = u.IdTipoEnte,
                        
                                 Activo = u.Activo,
                                 Pasos = u.Pasos
                                 //TipoAutorizacion = u.TipoAutorizacion
                             };
                }
                else
                {
                    result = from u in Set
                             select new FlujoViatico()
                             {
                                 Id = u.Id,
                                 IdTipoEnte = u.IdTipoEnte,
                             
                                 Activo = u.Activo,
                                 Pasos = u.Pasos,
                                 //TipoAutorizacion = u.TipoAutorizacion
                             };
                }

             
            }


            return result.Paginar(parametros == null ? 1 : parametros.Pagina, parametros == null ? 20 : parametros.ElementosPorPagina);
        }

        public override bool ExisteFlujoPredeterminado(int idTipoEnte)
        {

            var flujos = (from u in Set
                        where u.IdTipoEnte == idTipoEnte
                          select u).ToList();

            var existe = flujos.GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado)
                .Any(g => g.Count() == 1);

            return existe;
        }

        public override bool ExisteNivelRepetido(int idTipoEnte)
        {
           
            var flujos = (from u in Set
                          where u.IdTipoEnte == idTipoEnte
                          select u).ToList();


            var repetido = flujos.GroupBy(x => x.IdNivelEmpleado).Any(g => g.Count() > 1);

            return repetido;
        }

        public override bool ExisteRegistroEntePublico(FlujoViatico flujo)
        {
            return false;
        }

        public override List<FlujoViatico> ObtenerFlujos(List<FlujoViatico> flujos)
        {
            //if (id <= 0)
            //    return new ConfiguracionFlujo();

            //return (from u in Set
            //        where u.id ==flujos[0].IdTipoEnte
            //        select new
            //        {
            //            flujo = u,
            //            paso = u.Pasos.Where(r => r.Activo)
            //        }).ToList().Select(r => r.flujo);

            return new List<FlujoViatico>();

        }

        public List<FlujoViatico> ObtenerTotalFlujos(int idEntePublico)
        {
            var listaFlujos = (from u in Set
                               where u.IdTipoEnte == idEntePublico
                               select new
                               {
                                   flujo = u,
                                   paso = u.Pasos.Where(r => r.Activo)
                               }).Select(r => r.flujo).Where( x => x.Activo == true).ToList();

            return listaFlujos;
        }
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS