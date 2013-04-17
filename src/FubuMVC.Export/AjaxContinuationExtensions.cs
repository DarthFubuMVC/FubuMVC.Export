using FubuMVC.Core.Ajax;

namespace FubuMVC.Export
{
    public static class AjaxContinuationExtensions
    {
        public const string FileUrl = "fileUrl";

        public static AjaxContinuation DownloadFileUrl(this AjaxContinuation continuation, string fileUrl)
        {
            continuation[FileUrl] = fileUrl;
            return continuation;
        }

        public static string DownloadFileUrl(this AjaxContinuation continuation)
        {
            var fileUrl = string.Empty;

            if (continuation.HasData(FileUrl))
            {
                fileUrl = (string)continuation[FileUrl];
            }

            return fileUrl;
        }
    }
}