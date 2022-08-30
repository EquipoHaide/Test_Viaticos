
namespace Dominio.Nucleo.Entidades
{
    public interface ISeguimiento : ISeguimientoCreacion, ISeguimientoEliminacion, ISeguimientoModificacion
    {
        void Seguir(string idUsuario, bool esModificar, bool esEliminar);
    }
}