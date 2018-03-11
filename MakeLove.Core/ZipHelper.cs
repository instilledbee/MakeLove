using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace MakeLove.Core.Helpers
{
    /// <summary>
    /// Compression extension methods
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// Create a zip archive from the specified source path, according to a specified filter
        /// </summary>
        /// <param name="sourceDirectoryName"></param>
        /// <param name="destinationArchiveFileName"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="includeBaseDirectory"></param>
        /// <param name="filter"></param>
        /// <remarks>Source: https://stackoverflow.com/a/35416368/2335880 </remarks>
        public static void CreateFromDirectory(string sourceDirectoryName,
            string destinationArchiveFileName,
            CompressionLevel compressionLevel,
            bool includeBaseDirectory,
            Predicate<string> filter)
        {
            if (string.IsNullOrEmpty(sourceDirectoryName))
                throw new ArgumentNullException("sourceDirectoryName");

            if (string.IsNullOrEmpty(destinationArchiveFileName))
                throw new ArgumentNullException("destinationArchiveFileName");

            var filesToAdd = Directory.GetFiles(sourceDirectoryName, "*", SearchOption.AllDirectories);
            var entryNames = GetEntryNames(filesToAdd, sourceDirectoryName, includeBaseDirectory);

            using (var zipFileStream = new FileStream(destinationArchiveFileName, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    for (int i = 0; i < filesToAdd.Length; i++)
                    {
                        if (!filter(filesToAdd[i]))
                        {
                            continue;
                        }
                        archive.CreateEntryFromFile(filesToAdd[i], entryNames[i], compressionLevel);
                    }
                }
            }
        }

        private static string[] GetEntryNames(string[] names, string sourceFolder, bool includeBaseName)
        {
            if (names == null || names.Length == 0)
                return new string[0];

            if (includeBaseName)
                sourceFolder = Path.GetDirectoryName(sourceFolder);

            int length = string.IsNullOrEmpty(sourceFolder) ? 0 : sourceFolder.Length;

            if (length > 0
                    && sourceFolder != null && sourceFolder[length - 1] != Path.DirectorySeparatorChar 
                    && sourceFolder[length - 1] != Path.AltDirectorySeparatorChar)
                length++;

            var result = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                result[i] = names[i].Substring(length);
            }

            return result;
        }
    }
}
