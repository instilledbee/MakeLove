using MakeLove.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MakeLove.Core
{
    /// <summary>
    /// Creates a LOVE archive file from a directory of Lua files and assets
    /// </summary>
    public class Packer
    {
        private string _buildPath;
        private string _buildFileName;
        private string _gamePath;

        public Packer(string buildPath, string buildFileName, string gamePath)
        {
            _buildPath = buildPath;
            _buildFileName = buildFileName;
            _gamePath = gamePath;
        }

        public string CompressFiles()
        {
            DirectoryHelper.CreateIfNoneExists(_buildPath);

            var fullPath = Path.Combine(_buildPath, _buildFileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            ZipHelper.CreateFromDirectory(_gamePath, fullPath, CompressionLevel.Fastest, false, x => !String.IsNullOrEmpty(x));

            return fullPath;
        }
    }
}
