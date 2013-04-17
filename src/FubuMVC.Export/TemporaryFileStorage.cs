using System.IO;
using FubuCore;

namespace FubuMVC.Export
{
    public class TemporaryFileStorage : ITemporaryFileStorage
    {
        public string Create()
        {
            return Path.GetTempFileName();
        }

        public Stream Open(string path)
        {
            return File.Open(path, FileMode.Create);
        }

        public void MarkRead(string path)
        {
        }

        public string FileNameFromHash(string hash)
        {
            var path = Path.GetTempPath();
            var files = Directory.GetFiles(path);
            var fileName = string.Empty;

            foreach (var file in files)
            {
                if (file.ToHash() == hash)
                {
                    fileName = file;
                    break;
                }
            }

            return fileName;
        }
    }
}