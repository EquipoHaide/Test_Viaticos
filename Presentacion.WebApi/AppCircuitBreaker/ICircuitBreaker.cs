
namespace Presentacion.WebApi.AppCircuitBreaker
{
    public interface ICircuitBreaker
    {
        CircuitStates State { get; }
        void Up();
        void Down();
        void Starting();
        void OutOfService();
    }
}