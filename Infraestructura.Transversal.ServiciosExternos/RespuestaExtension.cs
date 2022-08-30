using Infraestructura.Transversal.Plataforma;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructura.Transversal.ServiciosExternos
{
    public static class RespuestaExtension
    {
        public static async Task<Respuesta<T>> FromResponseAsync<T>(this Respuesta<T> respuesta, HttpResponseMessage response)
        {
            var contenido = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<T>(contenido);

            return new Respuesta<T>(obj);
        }

        /*public static Respuesta Error(this Respuesta respuesta, string mensaje = null, string tag = null)
        {
            return new Respuesta(respuesta.ExcepcionInterna, mensaje ?? respuesta.Mensaje ?? "Ocurrio un error. [DB500]", tag ?? respuesta.TAG);
        }

        public static Respuesta<T> Error<T>(this Respuesta<T> respuesta, string mensaje = null, string tag = null)
        {
            return new Respuesta<T>(respuesta.ExcepcionInterna, mensaje ?? respuesta.Mensaje ?? "Ocurrio un error. [DB500]", tag ?? respuesta.TAG);
        }*/

        public static Respuesta ErrorServicioExterno(this Respuesta respuesta, string tag = null)
        {
            return new Respuesta(respuesta.ExcepcionInterna, "Ocurrio un error. [RQ500]", tag ?? respuesta.TAG);
        }

        public static Respuesta<T> ErrorServicioExterno<T>(this Respuesta<T> respuesta, string tag = null)
        {
            return new Respuesta<T>(respuesta.ExcepcionInterna, "Ocurrio un error. [RQ500]", tag ?? respuesta.TAG);
        }
    }
}