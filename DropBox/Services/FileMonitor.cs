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
        private IFileSystemWatcher _fileSystemWatcher;
        private readonly ILogger _logger;
        private readonly IFileSupport _fileSupport;

        public string PathToMonitor { get; set; }
        public string PathTarget { get; set; }

        public event EventHandler<DirectoryChangedEventArgs>? DirectoryChanged;

        public FileMonitor(string pathToMonitor, string pathTarget, ILogger logger, IFileSupport fileSupport, IFileSystemWatcher fileSystemWatcher)
        {
            this._fileSupport = fileSupport;
            this._logger = logger;
            this._fileSystemWatcher = fileSystemWatcher;
            this.PathToMonitor = pathToMonitor;
            this.PathTarget = pathTarget;

            logger.Log("Ready..");

            if (!Directory.Exists(pathToMonitor))
            {
                logger.Log("Dir error");
                throw new ArgumentException("The specified directory does not exist.", nameof(pathToMonitor));
            }

            ChangeInputPath(pathToMonitor);
        }

        private void OnFileAdded(object sender, FileSystemEventArgs e)
        {
            if (e.Name == null) return;
            OnDirectoryChanged(new DirectoryChangedEventArgs(e.ChangeType, e.FullPath));
            _logger.Log($"Adding.. {e.Name} to {PathToMonitor}");

            _fileSupport.CopyFile(e.FullPath, Path.Combine(PathTarget, e.Name));
        }

        private void OnFileRemoved(object sender, FileSystemEventArgs e)
        {
            OnDirectoryChanged(new DirectoryChangedEventArgs(e.ChangeType, e.FullPath));
            _logger.Log($"Removing.. {e.Name} from {PathToMonitor}");

            _fileSupport.DeleteFile(PathTarget + e.Name);
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
        public void ChangeInputPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            _fileSystemWatcher?.Dispose();
            _fileSystemWatcher = new FileSystemWatcherWrapper
            {
                Path = path,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                EnableRaisingEvents = true
            };

            _fileSystemWatcher.Created += OnFileAdded;
            _fileSystemWatcher.Deleted += OnFileRemoved;
        }

        public void ChangeTargetPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            PathTarget = path;
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
