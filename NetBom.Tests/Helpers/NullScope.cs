namespace NetBom.Tests.Helpers;

internal class NullScope : IDisposable
{
    public static NullScope Instance { get; } = new NullScope();

    private NullScope() { }

    public void Dispose() { }
}
