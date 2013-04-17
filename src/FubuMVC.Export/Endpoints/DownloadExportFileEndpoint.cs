using FubuMVC.Core.Behaviors;

namespace FubuMVC.Export.Endpoints
{
    public class DownloadExportFileModel
    {
        public string Hash { get; set; }
        public string NameToDisplay { get; set; }
    }

    public class DownloadExportFileEndpoint
    {
        private readonly ITemporaryFileStorage _temporaryFileStorage;

        public DownloadExportFileEndpoint(ITemporaryFileStorage temporaryFileStorage)
        {
            _temporaryFileStorage = temporaryFileStorage;
        }

        public DownloadFileModel get_export_download_Hash(DownloadExportFileModel input)
        {
            var fileName = _temporaryFileStorage.FileNameFromHash(input.Hash);
            var nameToDisplay = !string.IsNullOrWhiteSpace(input.NameToDisplay) ? input.NameToDisplay : "export.xlsx";

            return new DownloadFileModel
            {
                LocalFileName = fileName,
                FileNameToDisplay = nameToDisplay,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
