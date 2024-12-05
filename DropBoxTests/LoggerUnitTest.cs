using System.Collections.Generic;
using DropBox.Services;
using Moq;
using NUnit.Framework;

namespace DropBoxTests
{
    public class Tests
    {
        private Mock<ILogger> _mockLogger;
        private FileSupport _fileSupport;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger>();
            _fileSupport = new FileSupport(_mockLogger.Object);
        }

        [Test]
        public void CopyFile_ShouldLogFileSynced_WhenFileIsCopied()
        {
            var sourceFilePath = "C:\\Test\\Input\\La Ferrari.jpg";
            var destinationFilePath = "C:\\Test\\Output\\destination.txt";
            File.WriteAllText(sourceFilePath, "Test content");

            _fileSupport.CopyFile(sourceFilePath, destinationFilePath);

            _mockLogger.Verify(logger => logger.Log($"File Synced: {destinationFilePath}"), Times.Once);
            File.Delete(sourceFilePath);
            File.Delete(destinationFilePath);
        }

        [Test]
        public void CopyFile_ShouldLogFileAlreadyExists_WhenFileAlreadyExists()
        {
            var sourceFilePath = "C:\\Test\\Input\\La Ferrari.jpg";
            var destinationFilePath = "C:\\Test\\Output\\destination.txt";
            File.WriteAllText(sourceFilePath, "Test content");
            File.WriteAllText(destinationFilePath, "Test content");

            _fileSupport.CopyFile(sourceFilePath, destinationFilePath);

            _mockLogger.Verify(logger => logger.Log($"File already exists: {destinationFilePath}"), Times.Once);
            File.Delete(sourceFilePath);
            File.Delete(destinationFilePath);
        }

        [Test]
        public void CopyFile_ShouldLogError_WhenExceptionIsThrown()
        {
            var sourceFilePath = "C:\\Test\\Input\\La Ferrari.jpg";
            File.WriteAllText(sourceFilePath, "Test content");

            var invalidDestinationFilePath = "DSADSA:/destination.txt";

            _fileSupport.CopyFile(sourceFilePath, invalidDestinationFilePath);

            _mockLogger.Verify(logger => logger.Log(It.Is<string>(msg => msg.Contains("Error copying file"))), Times.Once);
            File.Delete(sourceFilePath);
        }

        [Test]
        public void DeleteFile_ShouldLogFileDeleted_WhenFileIsDeleted()
        {
            var filePath = "C:\\Test\\Input\\La Ferrari.jpg";
            File.WriteAllText(filePath, "Test content");

            _fileSupport.DeleteFile(filePath);

            _mockLogger.Verify(logger => logger.Log($"File deleted: {filePath}"), Times.Once);
            _mockLogger.Verify(logger => logger.Log($"File Synced: {filePath}"), Times.Once);
        }

        [Test]
        public void DeleteFile_ShouldLogFileNotFound_WhenFileDoesNotExist()
        {
            var filePath = "nonexistent.txt";

            _fileSupport.DeleteFile(filePath);

            _mockLogger.Verify(logger => logger.Log($"File not found: {filePath}"), Times.Once);
        }
    }
}
