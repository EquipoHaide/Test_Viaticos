using System;
using System.Threading;
using Serilog;
using Serilog.Core;

namespace Infraestructura.Transversal.Seguimiento
{
    public class ApplicationLogger : IApplicationLogger, IDisposable
    {
        //public LogEventLevel MinimumLevelLog { get; set; }
        public LoggingLevelSwitch LoggingLevelSwitch { get; set; }
        public Func<LoggingLevelSwitch, Logger> Build { get; set; }
        private Timer ReleaseLog { get; set; }
        private ILogger log;

        public ILogger Log
        {
            get
            {
                if (log == null)
                {
                    log = Build(LoggingLevelSwitch);
                    StartReleaseLogTimer();
                }
                return log;
            }
            set
            {
                if (value != null)
                {
                    log = value;
                    StartReleaseLogTimer();
                }
            }
        }

        public void Dispose()
        {
            ((Logger)log).Dispose();
            log = null;
        }

        private void StartReleaseLogTimer()
        {
            ReleaseLog = new Timer(state =>
            {
                if (log != null)
                {
                    ((Logger)log).Dispose();
                    log = null;
                }
            }, null, TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }
    }
}