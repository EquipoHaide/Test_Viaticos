
namespace Dominio.Nucleo
{
    public interface IModel
    {
        /// <summary>
        /// Valida toda la información de entrada proporcionada y revisa la información del modelo para garantizar la seguridad.
        /// </summary>
        bool IsValid();

    }
}