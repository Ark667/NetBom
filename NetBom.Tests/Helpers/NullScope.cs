/* MyDance Zone S.L. © 2023 */

namespace NetBom.Tests.Helpers;

internal class NullScope : IDisposable
{
    public static NullScope Instance { get; } = new NullScope();

    private NullScope() { }

    public void Dispose() { }
}
