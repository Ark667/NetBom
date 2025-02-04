using System.IO;

namespace NetBom.Core.Helpers;

public static class FileHelper
{
    public static void CreateDirectory(string path, bool clean = false)
    {
        if (clean && Directory.Exists(path))
            Directory.Delete(path, true);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}
