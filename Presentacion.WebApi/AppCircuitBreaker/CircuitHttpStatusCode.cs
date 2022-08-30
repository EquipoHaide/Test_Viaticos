
namespace Presentacion.WebApi.AppCircuitBreaker
{
    public enum CircuitHttpStatusCode
    {
        Up = 200,
        Down = 503,
        Starting = 512,
        OutOfService = 513
    }
}