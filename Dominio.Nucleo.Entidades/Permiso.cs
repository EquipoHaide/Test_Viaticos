using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dominio.Nucleo.Entidades
{
    public class Permiso : SeguimientoCreaModifica, IEntity, IPermiso
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdRol { get; set; }
        [Required]
        public int IdRecurso { get; set; }
        [Required]
        public bool EsLectura { get; set; }
        [Required]
        public bool EsEscritura { get; set; }
        [Required]
        public bool EsEjecucion { get; set; }
    }


    public static class Extensiones
    {
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsLectura activado.
        /// </summary>
        public static bool TieneLectura(this IEnumerable<IPermiso> permisos)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            return permisos.Where(p => p.EsLectura).Count() > 0;
        }
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsEscritura activado.
        /// </summary>
        public static bool TieneEscritura(this IEnumerable<IPermiso> permisos)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            return permisos.Where(p => p.EsEscritura).Count() > 0;
        }
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsEjecucion activado.
        /// </summary>
        public static bool TieneEjecucion(this IEnumerable<IPermiso> permisos)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            return permisos.Where(p => p.EsEjecucion).Count() > 0;
        }
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsLectura activado y un permiso con EsEscritura activado.
        /// </summary>
        public static bool EsEditable(this IEnumerable<IPermiso> permisos)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            return permisos.TieneLectura() && permisos.TieneEscritura();
        }
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsEjecucion y EsEscritura activados.
        /// </summary>
        public static bool EsEditableMismoRol(this IEnumerable<IPermiso> permisos)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            return permisos.Where(p => p.EsLectura && p.EsEscritura).Count() > 0;
        }
        /// <summary>
        /// Evalua que en la lista de permisos proporcionados exista almenos un permiso con EsEjecucion activado.
        /// (Condicion:esEscritura) Si el parametro EsLectura viene activo revisa que exista almenos un permiso con EsLectura Activo.
        /// (Condicion:esEscritura) Si el parametro esEscritura viene activo revisa que exista almenos un permiso con esEscritura Activo.
        /// </summary>
        public static bool EsAdministrable(this IEnumerable<IPermiso> permisos, bool esLectura, bool esEscritura, int idRecurso = 0)
        {
            if (permisos == null || permisos.Count() <= 0) return false;

            var permisosRecurso = idRecurso == 0 ? permisos : permisos.Where(p => p.IdRecurso == idRecurso);

            var esAdministrable = permisosRecurso.TieneEjecucion();

            if (esAdministrable && esLectura)
            {
                esAdministrable = permisosRecurso.TieneLectura();
            }
            if (esAdministrable && esEscritura)
            {
                esAdministrable = permisosRecurso.TieneEscritura();
            }

            return esAdministrable;
        }
    }
}