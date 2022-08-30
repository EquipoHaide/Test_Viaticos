
namespace Presentacion.WebApi.AppCircuitBreaker
{
    public enum CircuitStates
    {
        Up = 1,
        Down,
        Starting,
        OutOfService
    }
}