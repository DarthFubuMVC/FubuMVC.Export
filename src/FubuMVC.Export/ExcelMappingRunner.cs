using FubuCore;
using FubuCore.Formatting;
using FubuMVC.Core.Urls;
using FubuMVC.Export.Endpoints;

namespace FubuMVC.Export
{
    public interface IExcelMappingRunner
    {
        string GenerateFile(IExcelMapping mapping, object model);
        string UrlForFileName(string fileName);
    }

    public class ExcelMappingRunner : IExcelMappingRunner
    {
        private readonly IServiceLocator _locator;
        private readonly IDisplayFormatter _formatter;
        private readonly IExcelWriterFactory _writerFactory;
        private readonly IUrlRegistry _urlRegistry;

        public ExcelMappingRunner(IServiceLocator locator, IDisplayFormatter formatter, IExcelWriterFactory writerFactory, IUrlRegistry urlRegistry)
        {
            _locator = locator;
            _formatter = formatter;
            _writerFactory = writerFactory;
            _urlRegistry = urlRegistry;
        }

        public string GenerateFile(IExcelMapping mapping, object model)
        {
            var fileName = string.Empty;

            using (var config = _writerFactory.Build())
            {
                fileName = config.FileName;
                mapping.WriteTo(config.Writer, _formatter, _locator, model);
            }
            return fileName;
        }

        public string UrlForFileName(string fileName)
        {
            var empty = string.Empty;
            return _urlRegistry.UrlFor(new DownloadExportFileModel {Hash = fileName.ToHash()}, null);
        }
    }
}
