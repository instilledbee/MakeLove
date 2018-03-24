using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MakeLove.Core.Helpers
{
    internal static class DirectoryHelper
    {
        internal static void CreateIfNoneExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
