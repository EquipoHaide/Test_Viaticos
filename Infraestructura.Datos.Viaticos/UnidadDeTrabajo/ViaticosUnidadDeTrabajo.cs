using Microsoft.EntityFrameworkCore;


namespace Infraestructura.Datos.Viaticos.UnidadDeTrabajo
{
    public class ViaticosUnidadDeTrabajo : Infraestructura.Datos.Nucleo.UnidadDeTrabajo, IViaticosUnidadDeTrabajo
    {
        public ViaticosUnidadDeTrabajo() : base() { }

        public ViaticosUnidadDeTrabajo(DbContextOptions<ViaticosUnidadDeTrabajo> options) : base(options) { }
    }
}
