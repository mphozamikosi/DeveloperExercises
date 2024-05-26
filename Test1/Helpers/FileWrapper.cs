using System.Diagnostics.CodeAnalysis;

namespace Test1.Helpers
{
    [ExcludeFromCodeCoverage]
    public class FileWrapper : IFileWrapper
    {
        public FileWrapper() { }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
        public StreamReader OpenText(string path)
        {
            return File.OpenText(path);
        }
        public void CreateFile(string path)
        {
            File.Create(path);
        }
        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
