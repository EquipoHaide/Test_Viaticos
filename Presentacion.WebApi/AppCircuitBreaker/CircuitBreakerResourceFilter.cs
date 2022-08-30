using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentacion.WebApi.AppCircuitBreaker
{
    public class CircuitBreakerResourceFilter : IResourceFilter
    {
        ICircuitBreaker CircuitBreaker { get; set; }

        public CircuitBreakerResourceFilter(ICircuitBreaker circuitBreaker) => CircuitBreaker = circuitBreaker;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.ToString().Contains(CircuitBreakerConstants.PathConsole))
            {
                switch (CircuitBreaker.State)
                {
                    case CircuitStates.Down:

                        context.Result = new ContentResult
                        {
                            StatusCode = (int)CircuitHttpStatusCode.Down,
                            Content = "The service is down."
                        };

                        break;

                    case CircuitStates.Starting:

                        context.Result = new ContentResult
                        {
                            StatusCode = (int)CircuitHttpStatusCode.Starting,
                            Content = "The service is starting."
                        };

                        break;
                    case CircuitStates.OutOfService:

                        context.Result = new ContentResult
                        {
                            StatusCode = (int)CircuitHttpStatusCode.OutOfService,
                            Content = "The service is out of service."
                        };

                        break;
                }
            }

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}
