using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace MakeLove.Core
{
    /// <summary>
    /// Creates a Windows executable build for a LOVE game
    /// </summary>
    public class WindowsBuilder
    {
        private string _loveLibPath;
        private List<string> _loveFiles;
        private string _workingDir;

        public WindowsBuilder(string loveLibPath, string workingDirectory)
        {
            _loveLibPath = loveLibPath;
            _loveFiles = new List<string>()
            {
                @"love.dll",
                @"love.exe",
                @"lua51.dll",
                @"mpg123.dll",
                @"msvcp120.dll",
                @"msvcr120.dll",
                @"OpenAL32.dll",
                @"SDL2.dll"
            };

            _workingDir = workingDirectory;
            PrepareWorkingDirectory();
        }

        private void PrepareWorkingDirectory()
        {
            if(!Directory.Exists(_workingDir))
            {
                Directory.CreateDirectory(_workingDir);
            }
        }

        public void Build(string loveFilePath, string saveFileName)
        {
            var targetFileName = Path.Combine(_workingDir, saveFileName);
            var loveExePath = Path.Combine(_workingDir, _loveFiles.Single(x => x.Contains("exe")));

            using (var oldLoveFile = File.OpenRead(loveFilePath))
            using (var loveExe = File.OpenRead(loveExePath))
            using (var outputStream = File.Create(targetFileName))
            {
                loveExe.CopyTo(outputStream);
                oldLoveFile.CopyTo(outputStream);
            }
        }

        public void PrepareDependencies()
        {
            foreach(var libFile in _loveFiles)
            {
                var destFile = Path.Combine(_workingDir, libFile);

                if(!File.Exists(destFile))
                {
                    var origFile = Path.Combine(_loveLibPath, libFile);
                    File.Copy(origFile, destFile);
                }
            }
        }
    }
}
