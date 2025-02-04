using Microsoft.Extensions.Logging;

namespace NetBom.Tests.Helpers;

public class DebugLogger<T> : ILogger<T>
{
    IDisposable ILogger.BeginScope<TState>(TState state)
    {
        return NullScope.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception, string> formatter
    )
    {
        System.Diagnostics.Debug.WriteLine($"{DateTime.Now.TimeOfDay}--{typeof(T).Name}-->{state}");
    }
}
