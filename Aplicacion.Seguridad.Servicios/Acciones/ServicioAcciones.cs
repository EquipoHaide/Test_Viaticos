using Aplicacion.Nucleo;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Dominio.Seguridad.Modelos;
using Dominio.Seguridad.Repositorios;
using Infraestructura.Transversal.Plataforma;
using Infraestructura.Transversal.Plataforma.Extensiones;
using System;
using System.Collections.Generic;
using System.Linq;
using ServiciosDominio = Dominio.Seguridad.Servicios.Acciones;

namespace Aplicacion.Seguridad.Servicios
{
    class ServicioAcciones : Nucleo.ServicioRecursoBase<RecursoAccion>, IServicioAcciones
    {
        const string TAG = "Aplicacion.Seguridad.Servicios.Acciones.ServicioAcciones";

        Nucleo.IAplicacion App { get; set; }

        IServicioUsuarios servicioUsuario;
        IServicioUsuarios ServicioUsuario => App.Inject(ref servicioUsuario);

        ServiciosDominio.IServicioAcciones servicio;
        ServiciosDominio.IServicioAcciones Servicio => App.Inject(ref servicio);
        public override IServicioRecursoBase<RecursoAccion> ServicioDominio => Servicio;

        IRepositorioAcciones repositorio;
        IRepositorioAcciones Repositorio => App.Inject(ref repositorio);
        public override IRepositorioRecurso<RecursoAccion> RepositorioRecurso => this.Repositorio;

        Core.IServicioRoles servicioRoles;
        Core.IServicioRoles ServicioRoles => App.Inject(ref servicioRoles);
        public override IServicioRoles ServicioDeRoles => ServicioRoles;

        IRepositorioAccesos repositorioAccesos;
        IRepositorioAccesos RepositorioAccesos => App.Inject(ref repositorioAccesos);

        public ServicioAcciones(Nucleo.IAplicacion app)
        {
            App = app;
        }

        /// <summary>
        /// Obtiene una pagina de permisos del grupo que el usuario puede administrar filtrados por los parametros indicados.
        /// </summary>
        public override Respuesta<ConsultaPaginada<IPermisoModel>> ConsultarRecursos(IModeloConsultaRecurso parametros, string subjectId)
        {
            var opciones = (ConsultaRecursoAccionModelo)parametros;
            if (opciones == null || opciones.IdRol <= 0) return new Respuesta<ConsultaPaginada<IPermisoModel>>(R.strings.ModeloDeConsultaDeRecursosInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarRecursos(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<IPermisoModel>>(TAG);

            return new Respuesta<ConsultaPaginada<IPermisoModel>>(recursos.Contenido);
        }

        /// <summary>
        /// Valida el acceso del usiario a la acción indicada por su ruta.
        /// </summary>
        public Respuesta<bool> Validar(string accion, string subjectId)
        {
            var EsUsuarioHabilitado = ServicioUsuario.EsUsuarioHabilitado(subjectId);

            if (EsUsuarioHabilitado.EsError)
                return new Respuesta<bool>(false);

            if (!EsUsuarioHabilitado.Contenido)
                return new Respuesta<bool>(false);

            var acceso = Repositorio.Try(r => r.ObtenerAcceso(accion, subjectId));

            if (acceso.EsError)
                return acceso.ErrorBaseDatos<bool>(TAG);

            return Servicio.ValidarAcceso(acceso.Contenido);
        }

        public Respuesta<List<AccesoAsignado>> Validar(List<string> acciones, string subjectId)
        {
            var accesos = Repositorio.Try(r => r.ObtenerAccesos(acciones, subjectId));

            if (accesos.EsError)
                return accesos.ErrorBaseDatos<List<AccesoAsignado>>(TAG);

            return Servicio.ValidarAccesos(acciones, accesos.Contenido);
        }

        public Respuesta<List<Dominio.Seguridad.Modelos.Modulo>> ObtenerModulos(string subjectId)
        {
            var modulos = Repositorio.Try(ac => ac.ObtenerModulos(subjectId));

            if (modulos.EsError)
                return modulos.ErrorBaseDatos<List<Dominio.Seguridad.Modelos.Modulo>>(TAG);

            return new Respuesta<List<Dominio.Seguridad.Modelos.Modulo>>(modulos.Contenido);
        }

        public Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.Acceso>> ConsultarAccesos(ConsultaAcceso parametros, string subjectId)
        {
            var opciones = parametros;
            if (opciones == null || opciones.IdRol <= 0) return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.Acceso>>(R.strings.ModeloDeConsultaDeRecursosInvialido, TAG);

            var recursos = Repositorio.Try(r => r.ConsultarAccesos(parametros, subjectId));
            if (recursos.EsError) return recursos.ErrorBaseDatos<ConsultaPaginada<Dominio.Seguridad.Modelos.Acceso>>(TAG);

            return new Respuesta<ConsultaPaginada<Dominio.Seguridad.Modelos.Acceso>>(recursos.Contenido);
        }

        public Respuesta<List<Dominio.Seguridad.Modelos.Acceso>> AdministrarAccesos(IEnumerable<Dominio.Seguridad.Modelos.Acceso> accesos, string subjectId)
        {

            if (accesos == null || !accesos.Any() || accesos.Any(a => a == null))
                return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(R.strings.AccionesInvalidas, TAG);

            var idsAcciones = accesos.Select(m => m.IdAccion).ToList();

            var permisosAcciones = Repositorio.Try(m => m.ObtenerPermisosDeUsuarioAcciones(idsAcciones, subjectId));

            if (permisosAcciones.EsError)
                return permisosAcciones.ErrorBaseDatos<List<Dominio.Seguridad.Modelos.Acceso>>(permisosAcciones.TAG);

            var rolesDirectos = ServicioRoles.ObtenerRolesDirectos(subjectId);

            var respuestaValidacion = Servicio.ValidarPermisoAcciones(accesos.ToList().Select(m => m.IdAccion).ToList(), permisosAcciones.Contenido);

            if (respuestaValidacion.EsError)
                return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(respuestaValidacion.Mensaje, respuestaValidacion.TAG);

            var nuevos = new List<Dominio.Seguridad.Entidades.Acceso>();

            foreach (var acceso in accesos)
            {
                if (acceso.Id == 0 && acceso.EsAsignado)
                {

                    var respuestaNuevo = this.AgregarAcceso(acceso, subjectId);

                    if (respuestaNuevo.EsError)
                        return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(respuestaNuevo.Mensaje, respuestaNuevo.TAG);

                    nuevos.Add(respuestaNuevo.Contenido);

                }
                else if (acceso.Id > 0 && acceso.EsAsignado)
                {
                    var respuestaModificar = this.ModificarAcceso(acceso, subjectId);

                    if (respuestaModificar.EsError)
                        return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(respuestaModificar.Mensaje, respuestaModificar.TAG);
                }
                else if (acceso.Id > 0 && !acceso.EsAsignado)
                {
                    var respuestaEliminar = this.EliminarAcceso(acceso, subjectId);

                    if (respuestaEliminar.EsError)
                        return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(respuestaEliminar.Mensaje, respuestaEliminar.TAG);
                }
                else
                {
                    return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(R.strings.ErrorInesperado, TAG);
                }

            }

            var save = RepositorioAccesos.Try(r => r.Save());

            if (save.EsError)
                return save.ErrorBaseDatos<List<Dominio.Seguridad.Modelos.Acceso>>();

            return new Respuesta<List<Dominio.Seguridad.Modelos.Acceso>>(nuevos.Select(ac => ac.ToModel<Dominio.Seguridad.Modelos.Acceso>()).ToList());
        }

        #region Accesos

        private Respuesta<Dominio.Seguridad.Entidades.Acceso> AgregarAcceso(Dominio.Seguridad.Modelos.Acceso acceso, string subjectId)
        {

            var respuesta = Servicio.CrearAcceso(acceso?.ToEntity<Dominio.Seguridad.Entidades.Acceso>(), subjectId);

            if (respuesta.EsError)
                return new Respuesta<Dominio.Seguridad.Entidades.Acceso>(respuesta.Mensaje, respuesta.TAG);

            RepositorioAccesos.Add(respuesta.Contenido);


            return new Respuesta<Dominio.Seguridad.Entidades.Acceso>(respuesta.Contenido);

        }
        private Respuesta ModificarAcceso(Dominio.Seguridad.Modelos.Acceso acceso, string subjectId)
        {
            var obtenerAcceso = repositorio.Try(m => m.ObtenerAcceso(acceso?.Id ?? 0, subjectId));

            if (obtenerAcceso.EsError)
                return obtenerAcceso.ErrorBaseDatos();

            var accesoSincronizado = MainMapper.Instance.Mapper.Map<Dominio.Seguridad.Modelos.Acceso, Dominio.Seguridad.Entidades.Acceso>(acceso, obtenerAcceso.Contenido);

            var respuesta = Servicio.ModificarAcceso(accesoSincronizado, subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);

            RepositorioAccesos.Update(respuesta.Contenido);


            return new Respuesta();

        }
        private Respuesta EliminarAcceso(Dominio.Seguridad.Modelos.Acceso acceso, string subjectId)
        {

            var obtenerAcceso = Repositorio.Try(m => m.ObtenerAcceso(acceso?.Id ?? 0, subjectId));

            var respuesta = Servicio.EliminarAcceso(obtenerAcceso?.Contenido, subjectId);

            if (respuesta.EsError)
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);

            RepositorioAccesos.Update(respuesta.Contenido);


            return new Respuesta();

        }

        #endregion
    }
}
