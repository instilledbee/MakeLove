using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MakeLove.Core
{
    /// <summary>
    /// Monitors the source directory for any changes
    /// </summary>
    public class FileMonitor
    {
        private FileSystemWatcher _watcher;

        public event FileSystemEventHandler OnFileChange;

        public FileMonitor(string pathToMonitor)
        {
            _watcher = new FileSystemWatcher(pathToMonitor);

            _watcher.NotifyFilter = NotifyFilters.FileName | 
                                    NotifyFilters.CreationTime | 
                                    NotifyFilters.DirectoryName | 
                                    NotifyFilters.LastWrite | 
                                    NotifyFilters.Size;

            _watcher.Created += FileUpdated;
            _watcher.Changed += FileUpdated;
            _watcher.Deleted += FileUpdated;
            _watcher.Renamed += FileUpdated;
        }

        public void Start()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
        }

        private void FileUpdated(object sender, FileSystemEventArgs e)
        {
            OnFileChange(sender, e);
        }
    }
}
