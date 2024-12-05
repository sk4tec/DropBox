using System.IO;

namespace DropBox.Services
{
    public class FileSystemWatcherWrapper : IFileSystemWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;

        public FileSystemWatcherWrapper()
        {
            _fileSystemWatcher = new FileSystemWatcher();
        }

        public string Path
        {
            get => _fileSystemWatcher.Path;
            set => _fileSystemWatcher.Path = value;
        }

        public NotifyFilters NotifyFilter
        {
            get => _fileSystemWatcher.NotifyFilter;
            set => _fileSystemWatcher.NotifyFilter = value;
        }

        public bool EnableRaisingEvents
        {
            get => _fileSystemWatcher.EnableRaisingEvents;
            set => _fileSystemWatcher.EnableRaisingEvents = value;
        }

        public event FileSystemEventHandler Created
        {
            add => _fileSystemWatcher.Created += value;
            remove => _fileSystemWatcher.Created -= value;
        }

        public event FileSystemEventHandler Deleted
        {
            add => _fileSystemWatcher.Deleted += value;
            remove => _fileSystemWatcher.Deleted -= value;
        }

        public void Dispose()
        {
            _fileSystemWatcher.Dispose();
        }
    }
}

