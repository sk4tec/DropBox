using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropBox.Services
{   public interface IFileMonitor
    {
        event EventHandler<DirectoryChangedEventArgs> DirectoryChanged;

        void StopMonitoring();
        void ChangeInputPath(string path);
        void ChangeTargetPath(string path);
    }
}
