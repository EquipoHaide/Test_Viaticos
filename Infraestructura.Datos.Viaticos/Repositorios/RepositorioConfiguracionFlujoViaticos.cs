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

        public override bool ExisteFlujoPredeterminado(ConfiguracionFlujo flujo)
        {

            var flujos = (from u in Set
                        where u.IdEntePublico == flujo.IdEntePublico
                        select u).ToList();

            var existe = flujos.GroupBy(x => x.TipoFlujo == (int)TipoFlujo.Predeterminado)
                .Any(g => g.Count() == 1);

            return existe;
        }

        public override bool ExisteNivelRepetido(ConfiguracionFlujo flujo)
        {
            var flujos = (from u in Set
                        where u.IdEntePublico == flujo.IdEntePublico 
                        select u).ToList();

            //var existe = false;
            var existe = flujos.Exists(x => x.IdNivelEmpleado == flujo.IdNivelEmpleado);

            return existe;
        }

        public override bool ExisteRegistroEntePublico(ConfiguracionFlujo flujo)
        {
            return false;
        }

        public override ConfiguracionFlujo ObtenerFlujo(int id)
        {
            if (id <= 0)
                return new ConfiguracionFlujo();

            return (from u in Set
                    where u.Id == id
                    select new
                    {
                        flujo = u,
                        paso = u.Pasos.Where(r => r.Activo)
                    }).ToList().Select(r => r.flujo).FirstOrDefault();
                   
        }

        public List<ConfiguracionFlujo> ObtenerTotalFlujos(int idEntePublico)
        {
            var listaFlujos = (from u in Set
                               where u.IdEntePublico == idEntePublico
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