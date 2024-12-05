namespace DropBox.Services
{   public interface IFileMonitor
    {
        event EventHandler<DirectoryChangedEventArgs> DirectoryChanged;

        void StopMonitoring();
        void ChangeInputPath(string path);
        void ChangeTargetPath(string path);
    }
}
