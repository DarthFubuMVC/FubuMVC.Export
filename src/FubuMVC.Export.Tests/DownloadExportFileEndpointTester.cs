using FubuCore;
using FubuMVC.Export.Endpoints;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class DownloadExportFileEndpointTester
    {
        private TemporaryFileStorage theTempFileStorage;
        private DownloadExportFileEndpoint theEndpoint;

        [SetUp]
        public void Setup()
        {
            theTempFileStorage = new TemporaryFileStorage();
            theEndpoint = new DownloadExportFileEndpoint(theTempFileStorage);
        }

        [Test]
        public void should_return_the_temp_filename_from_hash()
        {
            var fileName = theTempFileStorage.Create();
            var model = new DownloadExportFileModel {Hash = fileName.ToHash()};
            var response = theEndpoint.get_export_download_Hash(model);

            response.LocalFileName.ShouldEqual(fileName);
            response.FileNameToDisplay.ShouldEqual("export.xlsx");
            response.ContentType.ShouldEqual("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [Test]
        public void should_use_the_NameToDisplay_if_given()
        {
            const string displayName = "mydisplayname.txt";
            var model = new DownloadExportFileModel {NameToDisplay = displayName};
            var response = theEndpoint.get_export_download_Hash(model);

            response.FileNameToDisplay.ShouldEqual(displayName);
        }
    }
}
