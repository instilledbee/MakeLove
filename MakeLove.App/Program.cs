using MakeLove.Core;
using System;
using System.IO;

namespace MakeLove.App
{
    class Program
    {
        //static readonly string LOVE_GAME_PATH = @"D:\Programming\Lua\fangirl\src";
        //static readonly string BUILD_PATH = @"D:\Programming\Lua\fangirl\bin";
        //static readonly string WINDOWS_BUILD_PATH = @"D:\Programming\Lua\fangirl\bin\windows";
        //static readonly string BUILD_NAME = @"Fangirls.love";
        //static readonly string LOVE_BIN_PATH = @"C:\Program Files\LOVE";

        static void Main(string[] args)
        {
            Console.WriteLine("MakeLove Automatic LOVE Build System");
            Console.WriteLine("App Version {0}", typeof(Program).Assembly.GetName().Version.ToString());
            Console.WriteLine("Core Version {0}", typeof(FileMonitor).Assembly.GetName().Version.ToString());
            Console.WriteLine("Written by InstilledBee");

            var monitor = new FileMonitor(ConfigHelper.SourcePath);
            monitor.OnFileChange += Monitor_OnFileChange;
            monitor.Start();

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }

        static void Monitor_OnFileChange(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Change detected: {0} {1}", e.FullPath, e.ChangeType);

            var packer = new Packer(ConfigHelper.BuildPath, ConfigHelper.SourcePath);
            var createdFilePath = packer.CompressFiles(ConfigHelper.LoveBuildName);

            var windowsBuilder = new WindowsBuilder(ConfigHelper.LovePath, ConfigHelper.WindowsBuildPath);
            windowsBuilder.PrepareDependencies();
            windowsBuilder.Build(createdFilePath, ConfigHelper.WindowsBuildName);
        }
    }
}
