using System.IO;

namespace FubuMVC.Export
{
    public interface ITemporaryFileStorage
    {
        string Create();
        Stream Open(string path);
        void MarkRead(string path);
        string FileNameFromHash(string hash);
    }
}