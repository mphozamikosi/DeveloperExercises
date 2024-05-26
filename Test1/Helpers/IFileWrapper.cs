namespace Test1.Helpers
{
    public interface IFileWrapper
    {
        bool FileExists(string path);
        StreamReader OpenText(string path); 
        void CreateFile(string path);
        void WriteAllText(string path, string content);
        void DeleteFile(string path);
    }
}
