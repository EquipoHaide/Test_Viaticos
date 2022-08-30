
namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            return false;
        }

        public static bool IsNotNullOrEmptyOrWhiteSpace(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return true;
                }
            }
            return false;
        }
    }
}