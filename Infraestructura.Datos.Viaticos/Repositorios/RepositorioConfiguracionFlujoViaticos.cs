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
    public class RepositorioConfiguracionFlujoViaticos : RepositorioConfiguracionFlujo<Entidades.ConfiguracionFlujo,ConsultaConfiguracionFlujo>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override ConsultaPaginada<ConfiguracionFlujo> ConsultarFlujosDeAutorizacion(ConsultaConfiguracionFlujo parametros, string subjectId)
        {

            IQueryable<ConfiguracionFlujo> result = null;
           
            if (parametros.Query.IsNotNullOrEmptyOrWhiteSpace())//Busqueda Generica
            {
                var parametro = parametros.Query.Trim().ToLower();

            
                result = from u in Set
                         where u.NombreFlujo.Trim().ToLower() == parametro
                         select new ConfiguracionFlujo()
                             {
                                 Id = u.Id,
                                 IdEntePublico = u.IdEntePublico,
                                 IdNivelEmpleado = u.IdNivelEmpleado,
                                 NombreFlujo = u.NombreFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos,
                                 TipoFlujo = u.TipoFlujo
                             };

            }
            else //Busqueda Avanzada
            {
                

                if (parametros.IdEntePublico > 0)
                {
                    result = from u in Set
                             where u.IdEntePublico == parametros.IdEntePublico
                             select new ConfiguracionFlujo()
                             {
                                 Id = u.Id,
                                 IdEntePublico = u.IdEntePublico,
                                 IdNivelEmpleado = u.IdNivelEmpleado,
                                 NombreFlujo = u.NombreFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos,
                                 TipoFlujo = u.TipoFlujo
                             };
                }
                else
                {
                    result = from u in Set
                             select new ConfiguracionFlujo()
                             {
                                 Id = u.Id,
                                 IdEntePublico = u.IdEntePublico,
                                 IdNivelEmpleado = u.IdNivelEmpleado,
                                 NombreFlujo = u.NombreFlujo,
                                 Activo = u.Activo,
                                 Pasos = u.Pasos,
                                 TipoFlujo = u.TipoFlujo
                             };
                }

             
            }


            return result.Paginar(parametros == null ? 1 : parametros.Pagina, parametros == null ? 20 : parametros.ElementosPorPagina);
        }

        public override bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {

            var flujo = (from u in Set
                        where u.IdEntePublico == idTipoEntePublico
                        select u).ToList();

            var existe = flujo.GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado)
                .Any(g => g.Count() > 0);

            return existe;
        }

        public override bool ExisteNivelRepetido(int idTipoEntePublico, int idNivel)
        {
            var flujo = (from u in Set
                        where u.IdEntePublico == idTipoEntePublico
                        select u).ToList();

            var existe = flujo.Exists(x => x.IdNivelEmpleado == idNivel);

            return existe;
        }

        public override ConfiguracionFlujo ObtenerFlujo(int id)
        {
            if (id <= 0)
                return new ConfiguracionFlujo();
          
            return (from u in Set
                   where u.Id == id
                   let pasos = (from p in u.Pasos
                                where p.IdConfiguracionFlujo == id
                                select p)
                   select new ConfiguracionFlujo()
                   {
                       Id = u.Id,
                       IdEntePublico = u.IdEntePublico,
                       IdNivelEmpleado = u.IdNivelEmpleado,
                       NombreFlujo = u.NombreFlujo,
                       Activo = u.Activo,
                       Pasos = pasos.ToList(),
                       TipoFlujo = u.TipoFlujo
                   }).FirstOrDefault();
        }


        //public override ConfiguracionFlujo ObtenerFlujos(ConfiguracionFlujo flujo, string subjectId)
        //{

        //    return flujo;
        //}

        //public override List<ConfiguracionFlujo> ObtenerFlujos(int idEntePublico)
        //{
        //    return new List<ConfiguracionFlujo>();
        //}
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS