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
        private string _lastFileName;
        private DateTime _lastWriteTime;

        public event FileSystemEventHandler OnFileChange;

        public FileMonitor(string pathToMonitor)
        {
            _watcher = new FileSystemWatcher(pathToMonitor);
            _watcher.IncludeSubdirectories = true;

            _watcher.NotifyFilter = NotifyFilters.FileName | 
                                    NotifyFilters.CreationTime | 
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
            var lastDetectedTime = File.GetLastWriteTime(e.FullPath);

            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (String.IsNullOrWhiteSpace(_lastFileName) || !_lastFileName.Equals(e.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    _lastFileName = e.Name;

                    if (_lastWriteTime != lastDetectedTime)
                    {
                        _lastWriteTime = lastDetectedTime;

                        OnFileChange(sender, e);
                    }
                }
            }
            else
            {
                OnFileChange(sender, e);
            }
        }
    }
}
