
namespace Dominio.Nucleo
{
    public interface IAplicacion
    {
        /// <summary>
        /// Registra una interface con su implementacion en el contenedor de la aplicación.
        /// </summary>
        void Register<TInterface, TService>()
            where TInterface : class
            where TService : class,
            TInterface;

        /// <summary>
        /// Realiza la inyeccion en el objeto de referencia.
        /// </summary>
        T Inject<T>(ref T value)
            where T : class;

        /// <summary>
        /// Permite resolver un objeto por medio de su tipo
        /// </summary>
        T Resolve<T>()
            where T : class;

    }
}