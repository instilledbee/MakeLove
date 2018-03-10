using System;
using System.IO;

namespace MakeLove.App
{
    class Program
    {
        const string LOVE_GAME_PATH = @"D:\Programming\Lua\fangirl";
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
        }

        static void FileUpdated(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine()
        }
    }
}
