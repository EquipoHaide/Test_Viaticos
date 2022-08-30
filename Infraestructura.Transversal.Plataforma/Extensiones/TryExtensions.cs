using System;

namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class TryExtensions
    {
        /// <summary>
        /// Ejecuta la acción indicada en el parametro y lo envuelve en un tryCatch, en dado caso de generar excepciones envuelve la excepción en una respuesta.
        /// </summary>
        public static Respuesta Try<TRepository>(this TRepository repository, Action<TRepository> action)
        {
            try
            {
                action.Invoke(repository);
                return new Respuesta();
            }
            catch (Exception ex)
            {
                return new Respuesta(ex, ex.Message, "");
            }
        }
        /// <summary>
        /// Ejecuta la función indicada en el parametro y lo envuelve en un tryCatch, en dado caso de generar excepciones envuelve la excepción en una respuesta.
        /// </summary>
        public static Respuesta<TResult> Try<TRepository, TResult>(this TRepository repository, Func<TRepository, TResult> function)
        {
            try
            {
                return new Respuesta<TResult>(function.Invoke(repository));
            }
            catch (Exception ex)
            {
                return new Respuesta<TResult>(ex, ex.Message, "");
            }
        }
    }
}
