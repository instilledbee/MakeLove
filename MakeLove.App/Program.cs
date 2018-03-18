using MakeLove.Core;
using System;
using System.IO;

namespace MakeLove.App
{
    class Program
    {
        static readonly string LOVE_GAME_PATH = @"D:\Programming\Lua\fangirl\src";
        static readonly string BUILD_PATH = @"D:\Programming\Lua\fangirl\bin";
        static readonly string WINDOWS_BUILD_PATH = @"D:\Programming\Lua\fangirl\bin\windows";
        static readonly string BUILD_NAME = @"Fangirls.love";
        static readonly string LOVE_BIN_PATH = @"C:\Program Files\LOVE";

        static void Main(string[] args)
        {
            Console.WriteLine("MakeLove Automatic LOVE Build System");
            Console.WriteLine("Version 0.1.0");
            Console.WriteLine("Written by InstilledBee");

            var monitor = new FileMonitor(LOVE_GAME_PATH);
            monitor.OnFileChange += Monitor_OnFileChange;
            monitor.Start();

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        static void Monitor_OnFileChange(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change detected: {0} {1}", e.FullPath, e.ChangeType);

            var packer = new Packer(BUILD_PATH, BUILD_NAME, LOVE_GAME_PATH);
            var createdFilePath = packer.CompressFiles();

            var windowsBuilder = new WindowsBuilder(LOVE_BIN_PATH, WINDOWS_BUILD_PATH);
            windowsBuilder.PrepareDependencies();
            windowsBuilder.Build(createdFilePath, "Fangirls.exe");
        }
    }
}
