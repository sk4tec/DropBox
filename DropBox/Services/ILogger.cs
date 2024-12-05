namespace DropBox.Services
{    public interface ILogger
    {
        void Log(string message);
        IEnumerable<string> GetLogEntries();
    }
}

