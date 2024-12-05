using System.IO;

namespace DropBox.Services
{
    public interface IFileSystemWatcher : IDisposable
    {
        string Path { get; set; }
        NotifyFilters NotifyFilter { get; set; }
        bool EnableRaisingEvents { get; set; }

        event FileSystemEventHandler Created;
        event FileSystemEventHandler Deleted;
    }
}
