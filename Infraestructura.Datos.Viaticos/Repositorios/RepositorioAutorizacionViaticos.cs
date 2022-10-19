using System;
using Entidad = Dominio.Viaticos.Entidades;
using Modelo = Dominio.Viaticos.Modelos;
using Dominio.Viaticos.Repositorios;
using Infraestructura.Datos.Nucleo.AutorizacionSolicitud;
using Infraestructura.Datos.Viaticos.UnidadDeTrabajo;
using Infraestructura.Transversal.Plataforma;
using System.Collections.Generic;
using Dominio.Viaticos.Entidades;
using System.Linq;
using Infraestructura.Transversal.Plataforma.Extensiones;
using Dominio.Nucleo.Entidades;

namespace Infraestructura.Datos.Viaticos.Repositorios
{
    public class RepositorioAutorizacionViaticos : RepositorioAutorizacionBase<Entidad.SolicitudCondensada, Entidad.Autorizacion, Modelo.ConsultaSolicitudes>, IRepositorioAutorizacionViaticos
    {
        
        public RepositorioAutorizacionViaticos(IViaticosUnidadDeTrabajo unitOfWork) : base(unitOfWork) { }

        public override ConsultaPaginada<Entidad.SolicitudCondensada> ConsultarAutorizaciones(Modelo.ConsultaSolicitudes parametros, string subjectId)
        {
            if (parametros == null)
                return null;

            var rolesUsuario = from ru in UnitOfWork.Set<RolDirectoVista>()
                               where ru.SubjectId == subjectId
                               select ru.IdRol;                     

            var query = from u in Set
                        where rolesUsuario.Contains(u.IdRol)
                               //&& u.Activo
                               //&& (u.Folio.Equals(filtro))
                        select new SolicitudCondensada()
                        {
                            Id = u.Id,
                            Concepto = u.Concepto,
                            Folio = u.Folio,
                            IdAutorizacion = u.IdAutorizacion,
                            IdRol = u.IdRol,
                            Orden = u.Orden,

                            Estado = u.Estado,
                            //FechaCreacion = u.FechaCreacion,
                            //IdUsuarioCreo = u.IdUsuarioCreo,
                        };

            //Busqueda Generica
            if (parametros.Query.IsNotNullOrEmptyOrWhiteSpace())
            {

                var filtro = parametros.Query.Trim().ToLower();
                query = from u in query
                        where (u.Folio.Equals(filtro))
                        select u;

            }
            else //Busqueda Avanzada
            {
                if (parametros.Folio.IsNotNullOrEmptyOrWhiteSpace())
                {
                    query = (from oi in query
                             where oi.Folio.ToLower().Contains(parametros.Folio.Trim().ToLower())
                                        select oi);
                }

                if (parametros.Estatus>0)
                {
                    query = (from oi in query
                             where oi.Estado==parametros.Estatus
                             select oi);
                }

                //PENDIENTE DEBIDO A QUE LA SOLICITUD NO TIENEN UN TIPO DE ENTE, para esto tendria que navega a la autorizacio
                //y a su vez al flujo
                //if (parametros.IdEntePublico > 0)
                //{
                //    query = (from oi in query
                //             where oi.t == parametros.Estatus
                //             select oi);
                //}




            }

            var result = query.AsEnumerable().OrderByDescending(f => f.FechaCreacion);

            return result.Paginar(parametros == null ? 1 : parametros.Pagina, parametros == null ? 20 : parametros.ElementosPorPagina);
        }

        public override List<Autorizacion> ObtenerAutorizacion(List<int> IdsAutorizacion)
        {
            List<Autorizacion> listaAutorizacion = new List<Autorizacion>();

            
            foreach (var Id in IdsAutorizacion)
            {
                var autorizacion = (from u in UnitOfWork.Set<Entidad.Autorizacion>()
                                         where u.Id == Id
                                         select u).FirstOrDefault();

                listaAutorizacion.Add(autorizacion);
            }
           

            return listaAutorizacion;

        }

        public override List<SolicitudCondensada> ObtenerSolicitudesCondensadas(List<int> IdsSolicitudes)
        {
            List<SolicitudCondensada> listaSolicitudes = new List<SolicitudCondensada>();

            foreach (var Id in IdsSolicitudes)
            {
                var solicitud = (from u in Set
                                   where u.Id == Id
                                   select u).FirstOrDefault();
                listaSolicitudes.Add(solicitud);
            }


            return listaSolicitudes;
        }
    }
}
