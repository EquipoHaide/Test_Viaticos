using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Presentacion.WebApi.AppCircuitBreaker;

namespace Presentacion.WebApi.HealthCheck
{
    public class AppHealthCheck : IHealthCheck
    {
        ICircuitBreaker CircuitBreaker { get; set; }

        public AppHealthCheck(ICircuitBreaker circuitBreaker) => CircuitBreaker = circuitBreaker;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            switch (CircuitBreaker.State)
            {
                case CircuitStates.Up:
                    return Task.FromResult(HealthCheckResult.Healthy());
                case CircuitStates.Down:
                    return Task.FromResult(HealthCheckResult.Unhealthy("The service is down."));
                case CircuitStates.Starting:
                    return Task.FromResult(HealthCheckResult.Unhealthy("The service is starting."));
                case CircuitStates.OutOfService:
                    return Task.FromResult(HealthCheckResult.Unhealthy("The service is out of service."));
                default:
                    return Task.FromResult(HealthCheckResult.Healthy());
            }
        }
    }
}