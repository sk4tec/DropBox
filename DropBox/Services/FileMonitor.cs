﻿using System;
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

        public event EventHandler<DirectoryChangedEventArgs> DirectoryChanged;

        public FileMonitor(string pathToMonitor)
        {
            if (!Directory.Exists(pathToMonitor))
            {
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
        }

        private void OnFileRemoved(object sender, FileSystemEventArgs e)
        {
            OnDirectoryChanged(new DirectoryChangedEventArgs(e.ChangeType, e.FullPath));
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