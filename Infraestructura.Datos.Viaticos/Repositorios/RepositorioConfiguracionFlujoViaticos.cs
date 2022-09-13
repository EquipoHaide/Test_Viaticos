using Dominio.Viaticos.Repositorios;
using MicroServices.Platform.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Dominio.Viaticos.Entidades;
using Infraestructura.Transversal.Plataforma;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioConfiguracionFlujoViaticos : Repository<Dominio.Viaticos.Entidades.FlujoViaticos>, IRepositorioConfiguracionFlujoViaticos
    {
        public RepositorioConfiguracionFlujoViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }


        public void AddFlujo(FlujoViaticos flujo)
        {
            UnitOfWork.Set<FlujoViaticos>().AddRange(flujo);
        }

        public Respuesta<ConsultaPaginada<IConsulta>> ConsultarFlujosDeAutorizacion(IConsulta parametros, string subjectId)
        {

             
            return new Respuesta<ConsultaPaginada<IConsulta>>("");
        }

        public bool ExisteFlujoPredeterminado(int idTipoEntePublico)
        {
            return false;
        }

        public bool ExisteNivelRepetido(int idTipoEntePublico, string nivel)
        {
            return false;
        }

        public FlujoViaticos ObtenerFlujos(FlujoViaticos flujo, string subjectId)
        {
            FlujoViaticos lista = null;
            return lista;
        }

        public void RemoverFlujo(FlujoViaticos flujo)
        {

            //var flujo = (from c in Set
            //             where id == c.id
            //             select c);
            UnitOfWork.Set<FlujoViaticos>().RemoveRange(flujo);


            //UnitOfWork.Set<FlujoViaticos>().AddRange(flujo);

        }

        
    }
}



//PENDIENTE PARA EL LUNES CONTINUNAR CON LA CREACION DE LA CARPTETA DE MIGRACIONS, SCRIPT DEL 
//PROYECTO DE INFREASTRUCTURA DE DATOS DE VIATICOS