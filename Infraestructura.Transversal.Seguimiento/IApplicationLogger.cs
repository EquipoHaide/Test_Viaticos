using Serilog;

namespace Infraestructura.Transversal.Seguimiento
{
    public interface IApplicationLogger
    {
        ILogger Log { get; set; }
    }
}