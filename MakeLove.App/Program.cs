using MakeLove.Core.Helpers;
using System;
using System.IO;
using System.IO.Compression;

namespace MakeLove.App
{
    class Program
    {
        const string LOVE_GAME_PATH = @"D:\Programming\Lua\fangirl";
        const string BUILD_PATH = @"D:\Programming\Lua\fangirl-build";
        const string BUILD_NAME = @"Fangirls.love";
        const string LOVE_BIN_PATH = @"C:\Program Files\LOVE";

        static void Main(string[] args)
        {
            Console.WriteLine("MakeLove Automatic LOVE Build System");
            Console.WriteLine("Version 0.1.0");
            Console.WriteLine("Written by InstilledBee");

            FileSystemWatcher watcher = new FileSystemWatcher(LOVE_GAME_PATH);
            watcher.Created += FileUpdated;
            watcher.Changed += FileUpdated;
            watcher.Deleted += FileUpdated;
            watcher.Renamed += FileUpdated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        static void FileUpdated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change detected: {0} {1}", e.FullPath, e.ChangeType);
            CompressFiles();
        }

        static void CompressFiles()
        {
            var fullPath = String.Format(@"{0}\{1}", BUILD_PATH, BUILD_NAME);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            ZipHelper.CreateFromDirectory(LOVE_GAME_PATH, fullPath, CompressionLevel.Fastest, false, x => !String.IsNullOrEmpty(x));
        }
    }
}
