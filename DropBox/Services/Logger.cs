using System;
using System.Collections.Generic;

namespace DropBox.Services
{
    public class Logger : ILogger
    {
        private readonly List<string> _logEntries;

        public Logger()
        {
            _logEntries = new List<string>();
        }

        public event EventHandler<string>? LogCreated;

        public void Log(string message)
        {
            _logEntries.Add(message);
            OnLogCreated(message);
        }

        public IEnumerable<string> GetLogEntries()
        {
            return _logEntries;
        }

        protected virtual void OnLogCreated(string logEntry)
        {
            LogCreated?.Invoke(this, logEntry);
        }
    }
}