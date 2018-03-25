using MakeLove.Core;
using System;
using System.IO;

namespace MakeLove.App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("MakeLove Automatic LOVE Build System");
                Console.WriteLine("App Version {0}", typeof(Program).Assembly.GetName().Version.ToString());
                Console.WriteLine("Core Version {0}", typeof(FileMonitor).Assembly.GetName().Version.ToString());
                Console.WriteLine("Written by InstilledBee");

                var monitor = new FileMonitor(ConfigHelper.SourcePath);
                monitor.OnFileChange += Monitor_OnFileChange;
                monitor.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to start application: {0}", ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to quit.");
                Console.ReadKey();
            }
        }

        static void Monitor_OnFileChange(object sender, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine("Change detected: {0} {1}", e.FullPath, e.ChangeType);

                // LOVE builds must be created at a minimum
                var packer = new Packer(ConfigHelper.BuildPath, ConfigHelper.SourcePath);
                var createdFilePath = packer.CompressFiles(ConfigHelper.LoveBuildName);

                if ((ConfigHelper.CurrentBuildTargets & BuildTargets.Windows) == BuildTargets.Windows)
                {
                    var windowsBuilder = new WindowsBuilder(ConfigHelper.LovePath, ConfigHelper.WindowsBuildPath);
                    windowsBuilder.PrepareDependencies();
                    windowsBuilder.Build(createdFilePath, ConfigHelper.WindowsBuildName);
                }

                if(ConfigHelper.UseBuildNumber)
                    ConfigHelper.IncrementBuildNumber();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error handling file change: {0}", ex.Message);
            }
        }
    }
}
