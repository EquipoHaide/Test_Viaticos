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
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujoBase<Entidades.FlujoViatico,ConsultaConfiguracionFlujo>, IRepositorioConfiguracionFlujoViaticos
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
                                 TipoFlujo = u.TipoFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos.OrderBy(x => x.Orden).Where(p => p.Activo == true).ToList(),
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
                                 TipoFlujo = u.TipoFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos.OrderBy(x => x.Orden).Where(p => p.Activo == true).ToList()
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
                                 TipoFlujo = u.TipoFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos.OrderBy(x => x.Orden).Where(p => p.Activo == true).ToList()
                                 //TipoAutorizacion = u.TipoAutorizacion
                             };
                }

             
            }


            return result.Paginar(parametros == null ? 1 : parametros.Pagina, parametros == null ? 20 : parametros.ElementosPorPagina);
        }

        public override bool ExisteFlujoPredeterminado(int idTipoEnte)
        {

            var f = (from u in Set
                    select u).ToList();

            var flujos = (from u in Set
                          where u.IdTipoEnte == idTipoEnte && u.Activo
                          select u).ToList();

            var existe = flujos.GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado)
                .Any(g => g.Count() == 1);


            //var repetido = (from u in UnitOfWork.Set<Entidades.FlujoViatico>()
            //                where u.IdTipoEnte == idTipoEnte
            //                select u).ToList().GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado).Any(a => a.Count() > 1);

            return existe;
        }

        public override bool ExisteNivelRepetido(int idTipoEnte)
        {
           
            var repetido = (from u in Set
                          where u.IdTipoEnte == idTipoEnte
                          select u).ToList().GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado).Any(a => a.Count() >  1);


           //var repetido = flujos.GroupBy(x => x.IdNivelEmpleado).Any(g => g.Count() > 1);

            return repetido;
        }


        
        public override bool ExisteRegistroEntePublico(FlujoViatico flujo)
        {

            //var existe = (from u in Entidades.TipoEntePublico
            //             where u.Id == flujo.IdTipoEnte
            //             select u)


            //var query = (from a in db1
            //             join b in db2 on a.EnteredBy equals b.UserId
            //             where a.LHManifestNum == LHManifestNum
            //             select new { LHManifestId = a.LHManifestId, LHManifestNum = a.LHManifestNum, LHManifestDate = a.LHManifestDate, StnCode = a.StnCode, Operatr = b.UserName }).FirstOrDefault();

            var tipoEnte = (from e in UnitOfWork.Set<Entidades.TipoEntePublico>()
                            select e).Any(r => r.Id == flujo.IdTipoEnte);


            return tipoEnte;
        }

        public override FlujoViatico ObtenerConfiguracionFlujo(int idFlujo)
        {
            var f = (from u in Set
                     where u.Id == idFlujo && u.Activo == true
                     select new FlujoViatico
                     {
                         Id = u.Id,
                         IdTipoEnte = u.IdTipoEnte,
                         IdNivelEmpleado = u.IdNivelEmpleado,
                         TipoFlujo = u.TipoFlujo,
                         Pasos = u.Pasos.Where(r => r.Activo).ToList()
                     }).FirstOrDefault();

            return f; 
        }

        public override List<FlujoViatico> ObtenerFlujosPorEntePublico(int idTipoEnte)
        {
            var lista = (from u in Set
                         where u.IdTipoEnte == idTipoEnte && u.Activo == true
                         select new
                         {
                             flujos = u,
                             paso = u.Pasos.Where(r => r.Activo)
                         }).ToList().Select(r => r.flujos);


            return lista.ToList();
         
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