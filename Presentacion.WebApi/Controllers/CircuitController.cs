using Microsoft.AspNetCore.Mvc;
using Presentacion.WebApi.AppCircuitBreaker;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentacion.WebApi.Controllers
{
    [ApiController]
    [Route(CircuitBreakerConstants.PathConsole)]
    public class CircuitController : ControllerBase
    {
        ICircuitBreaker CircuitBreaker { get; set; }

        public CircuitController(ICircuitBreaker circuitBreaker) => CircuitBreaker = circuitBreaker;

        [HttpPost]
        public IActionResult Post(CircuitBreakerBodyRequest estado)
        {
            switch (estado.CircuitState)
            {
                case CircuitStates.Up:
                    CircuitBreaker.Up();
                    return Ok();
                case CircuitStates.Down:
                    CircuitBreaker.Down();
                    return Ok();
                case CircuitStates.Starting:
                    CircuitBreaker.Starting();
                    return Ok();
                case CircuitStates.OutOfService:
                    CircuitBreaker.OutOfService();
                    return Ok();
                default:
                    return BadRequest();
            }

        }
    }
}