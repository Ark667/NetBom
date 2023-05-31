/* MyDance Zone S.L. © 2023 */

namespace NetBom.Tests.Helpers;

using Microsoft.Extensions.Logging;

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
