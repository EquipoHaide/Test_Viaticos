using System;
using Serilog.Events;

namespace Infraestructura.Transversal.Seguimiento
{
    public class LogEvent
    {
        protected IApplicationLogger Logger;
        public Type LogContext { get; private set; }

        public LogEvent(IApplicationLogger logger)
        {
            Logger = logger ?? throw new ArgumentException("El logger esta nulo.", nameof(logger));
        }

        public LogEvent(IApplicationLogger logger, Type logContext) : this(logger)
        {
            LogContext = logContext ?? throw new ArgumentException("El logContext esta nullo.", nameof(logger));
        }

        public LogEvent(IApplicationLogger logger, object logContext) : this(logger, logContext?.GetType())
        {
        }

        #region Metodos

        protected void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Verbose, messageTemplate, propertyValues);
        }

        protected void Debug(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Debug, messageTemplate, propertyValues);
        }

        protected void Information(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Information, messageTemplate, propertyValues);
        }

        protected void Warning(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Warning, messageTemplate, propertyValues);
        }

        protected void Error(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Error, messageTemplate, propertyValues);
        }

        protected void Fatal(string messageTemplate, params object[] propertyValues)
        {
            Write(LogEventLevel.Fatal, messageTemplate, propertyValues);
        }

        protected void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            using (Serilog.Context.LogContext.PushProperty("Origen", LogContext, true))
            {
                Logger.Log.Write(level, messageTemplate, propertyValues);
            }
        }

        #endregion
    }
}
