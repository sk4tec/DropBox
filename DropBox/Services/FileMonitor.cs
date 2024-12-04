using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBox.Services
{
    public class FileMonitor : IFileMonitor
    {
        private FileSystemWatcher _fileSystemWatcher;
        private ILogger _logger;
        private FileSupport _fileSupport;
        
        private string _pathToMonitor;
        private string _pathTarget;

        public event EventHandler<DirectoryChangedEventArgs> DirectoryChanged;

        public FileMonitor(string pathToMonitor, string pathTarget, ILogger logger, FileSupport fileSupport)
        {
            this._fileSupport = fileSupport;
            this._logger = logger;
            this._pathToMonitor = pathToMonitor;
            this._pathTarget = pathTarget;

            logger.Log("Started..");

            if (!Directory.Exists(pathToMonitor))
            {
                logger.Log("Dir error");
                throw new ArgumentException("The specified directory does not exist.", nameof(pathToMonitor));
            }

            _fileSystemWatcher = new FileSystemWatcher
            {
                Path = pathToMonitor,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                EnableRaisingEvents = true
            };

            _fileSystemWatcher.Created += OnFileAdded;
            _fileSystemWatcher.Deleted += OnFileRemoved;
        }

        private void OnFileAdded(object sender, FileSystemEventArgs e)
        {
            OnDirectoryChanged(new DirectoryChangedEventArgs(e.ChangeType, e.FullPath));
            _logger.Log($"Adding.. {e.Name} to {_pathToMonitor}");

            _fileSupport.CopyFile(e.FullPath, Path.Combine(_pathTarget, e.Name));
        }

        private void OnFileRemoved(object sender, FileSystemEventArgs e)
        {
            OnDirectoryChanged(new DirectoryChangedEventArgs(e.ChangeType, e.FullPath));
            _logger.Log($"Removing.. {e.Name} from {_pathToMonitor}");

            _fileSupport.DeleteFile(_pathTarget + e.Name);
        }

        protected virtual void OnDirectoryChanged(DirectoryChangedEventArgs e)
        {
            DirectoryChanged?.Invoke(this, e);
        }

        public void StopMonitoring()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Dispose();
        }
    }

    public class DirectoryChangedEventArgs : EventArgs
    {
        public WatcherChangeTypes ChangeType { get; }
        public string FilePath { get; }

        public DirectoryChangedEventArgs(WatcherChangeTypes changeType, string filePath)
        {
            ChangeType = changeType;
            FilePath = filePath;
        }
    }
}
