using System.Collections.Generic;
using FubuMVC.Core.Ajax;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Export
{
    public class ExcelMediaWriter<T> : IMediaWriter<T>
    {
        private readonly IExcelMapping _mapping;
        private readonly IJsonWriter _jsonWriter;
        private readonly IExcelMappingRunner _runner;

        public ExcelMediaWriter(
            IExcelMapping mapping,
            IJsonWriter jsonWriter,
            IExcelMappingRunner runner)
        {
            _mapping = mapping;
            _jsonWriter = jsonWriter;
            _runner = runner;
        }

        public void Write(string mimeType, T resource)
        {
            var fileName = _runner.GenerateFile(_mapping, resource);
            var fileUrl = _runner.UrlForFileName(fileName);
            var cont = AjaxContinuation.Successful().DownloadFileUrl(fileUrl);
            var values = cont.ToDictionary();
            _jsonWriter.Write(values, mimeType);
        }

        public IEnumerable<string> Mimetypes
        {
            get
            {
                yield return "application/json";
            }
        }
    }
}
