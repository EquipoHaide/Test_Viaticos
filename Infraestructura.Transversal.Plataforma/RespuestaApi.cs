
namespace Infraestructura.Transversal.Plataforma
{
    public class RespuestaApi<TContent>
    {
        public RespuestaApi(bool success, TContent content, string message = "")
        {
            Success = success;
            Content = content;
            Message = message;
        }

        public RespuestaApi(TContent content) : this(true, content) { }

        public RespuestaApi(string message)
        {
            Success = false;
            Fatal = false;
            Message = message;
        }

        /*public ApiResponse(Exception exception)
        {
            Success = false;
            Fatal = true;
            Message = exception.Message;
        }*/

        public bool Success { get; set; }
        public bool Fatal { get; set; }
        public TContent Content { get; set; }
        public string Message { get; set; }
    }

    public class RespuestaApi
    {
        public RespuestaApi() : this(true, "") { }

        public RespuestaApi(bool success, string message = "")
        {
            Success = success;
            Fatal = false;
            Message = message;
        }

        public RespuestaApi(string message)
        {
            Success = false;
            Fatal = false;
            Message = message;
        }

        /*public ApiResponse(Exception exception)
        {
            Success = false;
            Fatal = true;
            Message = exception.Message;
        }*/

        public RespuestaApi(string message, string id)
        {
            Success = false;
            Fatal = true;
            Message = $"{id} : {message}";
        }

        public bool Success { get; set; }
        public bool Fatal { get; set; }
        public string Message { get; set; }
    }
}