
namespace Presentacion.WebApi.AppCircuitBreaker
{
    public class CircuitBreakerBodyRequest
    {
        public int State { get; set; }
        public CircuitStates CircuitState { get => (CircuitStates)State; }
    }
}