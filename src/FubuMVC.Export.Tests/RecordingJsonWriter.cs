using FubuMVC.Core.Runtime;

namespace FubuMVC.Export.Tests.Infrastructure.Conneg
{
    public class RecordingJsonWriter : IJsonWriter
    {
        public object Output { get; set; }
        public string MimeType { get; set; }

        public void Write(object output)
        {
            throw new System.NotImplementedException();
        }

        public void Write(object output, string mimeType)
        {
            Output = output;
            MimeType = mimeType;
        }
    }
}