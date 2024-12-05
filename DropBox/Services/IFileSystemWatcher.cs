using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
