using Dominio.Nucleo.Servicios;
using Dominio.Seguridad.Entidades;
using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguridad.Servicios.Acciones
{
    public class ServicioAcciones : ServicioRecursoBase<RecursoAccion>, IServicioAcciones
    {
        private new const string TAG = "Dominio.Seguridad.Servicios.ServicioAcciones";

        /// <summary>
        /// Valida que exista un acceso para la ruta.
        /// </summary>
        public Respuesta<bool> ValidarAcceso(Acceso acceso)
        {
            if (acceso == null)
                return new Respuesta<bool>(false);

            if (acceso.FechaCaducidad <= DateTime.Now)
                return new Respuesta<bool>(false);

            return new Respuesta<bool>(true);
        }

        public Respuesta<List<Modelos.AccesoAsignado>> ValidarAccesos(List<string> acciones, List<Acceso> accesos)
        {
            var accesosValidados = new List<Modelos.AccesoAsignado>();

            foreach (var accion in acciones)
            {
                var acceso = accesos.Find(acc => acc.Accion.Ruta.ToLower() == accion.ToLower());

                accesosValidados.Add(
                    acceso != null
                        ? new Modelos.AccesoAsignado
                        {
                            IdAccion = acceso.IdAccion,
                            Accion = acceso.Accion.Nombre,
                            Ruta = acceso.Accion.Ruta,
                            EsAsignado = true
                        }
                        : new Modelos.AccesoAsignado
                        {
                            IdAccion = 0,
                            Accion = "",
                            Ruta = accion,
                            EsAsignado = false
                        }
                    );
            }

            return new Respuesta<List<Modelos.AccesoAsignado>>(accesosValidados);
        }

        public Respuesta ValidarPermisoAcciones(List<int> idsAccesos, IEnumerable<RecursoAccion> recursos)
        {
            var idsConPermisos = recursos.Where(m => m.EsLectura && m.EsEjecucion && idsAccesos.Contains(m.IdRecurso)).ToList().Select(m => m.IdRecurso).ToList().Distinct();

            var autorizado = idsAccesos.All(r => idsConPermisos.Contains(r));

            if (!autorizado)
                return new Respuesta(R.strings.UsuarioNoTienePermisos, TAG);

            return new Respuesta();
        }

        /// <summary>
        /// crea un acceso
        /// </summary>
        /// <param name="acceso"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public Respuesta<Acceso> CrearAcceso(Acceso acceso, string subjectId)
        {
            if (acceso == null)
                return new Respuesta<Acceso>(R.strings.AccesoNulo, TAG);

            if (acceso?.FechaCaducidad is null)
            {
                acceso.FechaCaducidad = DateTime.Now.AddDays(1).AddHours(24);
            }

            if (acceso.FechaCaducidad <= DateTime.Now)
                return new Respuesta<Acceso>(R.strings.FechaCaducidadInvalida, TAG);

            acceso.Seguir(subjectId);

            return new Respuesta<Acceso>(acceso);
        }

        /// <summary>
        /// Modifica un acceso
        /// </summary>
        /// <param name="acceso"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public Respuesta<Acceso> ModificarAcceso(Acceso acceso, string subjectId)
        {
            if (acceso == null)
                return new Respuesta<Acceso>(R.strings.AccesoNulo, TAG);

            if (acceso?.FechaCaducidad is null)
            {
                acceso.FechaCaducidad = DateTime.Now.AddDays(1).AddHours(24);
            }

            if (acceso.FechaCaducidad <= DateTime.Now)
                return new Respuesta<Acceso>(R.strings.FechaCaducidadInvalida, TAG);

            acceso.Seguir(subjectId, true, false);

            return new Respuesta<Acceso>(acceso);
        }

        /// <summary>
        /// Elimina un acceso
        /// </summary>
        /// <param name="acceso"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public Respuesta<Acceso> EliminarAcceso(Acceso acceso, string subjectId)
        {
            if (acceso == null)
                return new Respuesta<Acceso>(R.strings.AccesoNulo, TAG);

            acceso.Activo = false;
            acceso.Seguir(subjectId, true);

            return new Respuesta<Acceso>(acceso);
        }

    }
}
