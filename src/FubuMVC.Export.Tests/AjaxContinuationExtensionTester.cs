using FubuMVC.Core.Ajax;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class AjaxContinuationExtensionTester
    {
        [Test]
        public void should_append_file_url_property()
        {
            var fileUrl = "someUrl";
            var continuation = AjaxContinuation.Successful().DownloadFileUrl(fileUrl);
            continuation.DownloadFileUrl().ShouldBeTheSameAs(fileUrl);
        }
    }
}
