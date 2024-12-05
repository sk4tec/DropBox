namespace DropBox.Services
{
    public interface IFileSupport
    {
        void CopyFile(string sourceFilePath, string destinationFilePath);
        void DeleteFile(string filePath);
    }
}
