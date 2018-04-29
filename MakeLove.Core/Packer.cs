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
        private string _gamePath;

        public Packer(string buildPath, string gamePath)
        {
            _buildPath = buildPath;
            _gamePath = gamePath;
        }

        public string CompressFiles(string buildFileName, List<string> ignoredFileExts = null, List<string> ignoredFileNames = null)
        {
            // handle the list as an optional parameter
            ignoredFileExts = ignoredFileExts ?? new List<string>();
            ignoredFileNames = ignoredFileNames ?? new List<string>();

            DirectoryHelper.CreateIfNoneExists(_buildPath);
            
            var fullPath = Path.Combine(_buildPath, buildFileName);

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            ZipHelper.CreateFromDirectory(_gamePath, fullPath, CompressionLevel.Fastest, false, x => CheckFile(x, ignoredFileExts, ignoredFileNames));

            return fullPath;
        }

        private bool CheckFile(string fileName, List<string> ignoredFileExts, List<string> ignoredFileNames)
        {
            string ext = Path.GetExtension(fileName);

            return !String.IsNullOrEmpty(fileName) &&
                    !ignoredFileExts.Contains(ext) &&
                    !ignoredFileNames.Contains(fileName);
        }
    }
}
