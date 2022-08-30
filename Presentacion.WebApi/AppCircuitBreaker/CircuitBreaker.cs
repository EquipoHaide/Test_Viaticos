
namespace Presentacion.WebApi.AppCircuitBreaker
{
    public class CircuitBreaker : ICircuitBreaker
    {
        public CircuitStates State { get; private set; }

        public CircuitBreaker()
        {
            Starting();
        }

        public void Up()
        {
            State = CircuitStates.Up;
        }

        public void Down()
        {
            State = CircuitStates.Down;
        }

        public void Starting()
        {
            State = CircuitStates.Starting;
        }

        public void OutOfService()
        {
            State = CircuitStates.OutOfService;
        }
    }
}