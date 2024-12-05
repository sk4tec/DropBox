using System;
using System.IO;
using System.Runtime.InteropServices.Swift;

namespace DropBox.Services
{
    public class FileSupport: IFileSupport
    {
        private readonly ILogger _logger;

        public FileSupport(ILogger logger)
        {
            _logger = logger;
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                if (!File.Exists(destinationFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                    _logger.Log($"File Synced: {destinationFilePath}");
                }
                else
                {
                    _logger.Log($"File already exists: {destinationFilePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error copying file from {sourceFilePath} to {destinationFilePath}. Exception: {ex.Message}");
            }
        }

        public void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.Log($"File deleted: {filePath}");
                    _logger.Log($"File Synced: {filePath}");
                }
                else
                {
                    _logger.Log($"File not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error deleting file: {filePath}. Exception: {ex.Message}");
            }
        }
    }
}

