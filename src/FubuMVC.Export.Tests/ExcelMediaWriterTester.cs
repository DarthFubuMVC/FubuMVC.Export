using System.Collections.Generic;
using FubuMVC.Core.Runtime;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class ExcelMediaWriterTester : InteractionContext<ExcelMediaWriter<ExcelMediaWriterTester.MyModel>>
    {
        private IJsonWriter theJsonWriter;
        private IExcelMappingRunner theRunner;
        private const string theFileUrl = "/some/url";
        private const string theFileName = "something.tmp";

        [SetUp]
        public void Setup()
        {
            theJsonWriter = MockFor<IJsonWriter>();
            theRunner = MockFor<IExcelMappingRunner>();

            theRunner.Stub(x => x.GenerateFile(Arg<IExcelMapping>.Is.Anything, Arg<object>.Is.Anything))
                     .Return(theFileName);
            theRunner.Stub(x => x.UrlForFileName(Arg<string>.Is.Equal(theFileName))).Return(theFileUrl);
        }

        [Test]
        public void write_should_be_called()
        {
            const string mimeType = "application/xlsx";
            var model = new MyModel {};
            var arguments = theJsonWriter
                .CaptureArgumentsFor(x => x.Write(null, null));

            ClassUnderTest.Write(mimeType, model);

            var output = arguments.First<IDictionary<string, object>>();
            output["fileUrl"].ShouldNotBeNull();
            output["fileUrl"].ShouldEqual(theFileUrl);
            arguments.Second<string>().ShouldBeTheSameAs("application/json");
            theJsonWriter.AssertWasCalled(x => x.Write(Arg<object>.Is.Anything, Arg<string>.Is.Equal("application/json")));
        }

        [Test]
        public void mimetypes_should_be_correct()
        {
            ClassUnderTest.Mimetypes.Join(", ").ShouldEqual("application/xlsx, application/json");
        }

        public class MyModel
        {
            public string Name { get; set; }
        }
    }
}
