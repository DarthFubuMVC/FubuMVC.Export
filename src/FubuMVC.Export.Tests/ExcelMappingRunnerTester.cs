using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Urls;
using FubuMVC.Export.Endpoints;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class ExcelMappingRunnerTester : InteractionContext<ExcelMappingRunner>
    {
        private IExcelWriterFactory theExcelWriterFactory;
        private IExcelMapping theMapping;
        private ExcelWriterResponse theResponse;

        [SetUp]
        public void Setup()
        {
            theMapping = MockFor<IExcelMapping>();

            theResponse = new ExcelWriterResponse
            {
                Writer = MockFor<IExcelWriter>(),
                FileName = "temp.tmp"
            };

            theExcelWriterFactory = MockFor<IExcelWriterFactory>();
            theExcelWriterFactory
                .Stub(x => x.Build())
                .Return(theResponse);
        }

        [Test]
        public void should_hash_the_filename()
        {
            const string filename = "file.tmp";
            var registry = new RecordingUrlRegistry();
            var sut = new ExcelMappingRunner(null, null, null, registry);
            sut.UrlForFileName(filename);
            var model = (DownloadExportFileModel)registry.Model;
            model.Hash.ShouldEqual(filename.ToHash());
        }

        [Test]
        public void should_return_the_filename_from_the_builder()
        {
            var model = new object();

            var fileName = ClassUnderTest.GenerateFile(theMapping, model);
            fileName.ShouldBeTheSameAs(theResponse.FileName);
        }

        public class RecordingUrlRegistry : IUrlRegistry
        {
            public object Model { get; set; }

            public string UrlFor(object model, string categoryOrHttpMethod = null)
            {
                Model = model;
                return string.Empty;
            }

            public string UrlFor<T>(string categoryOrHttpMethod = null) where T : class
            {
                throw new NotImplementedException();
            }

            public string UrlFor(Type handlerType, MethodInfo method = null, string categoryOrHttpMethodOrHttpMethod = null)
            {
                throw new NotImplementedException();
            }

            public string UrlFor<TController>(Expression<Action<TController>> expression, string categoryOrHttpMethod = null)
            {
                throw new NotImplementedException();
            }

            public string UrlForNew(Type entityType)
            {
                throw new NotImplementedException();
            }

            public bool HasNewUrl(Type type)
            {
                throw new NotImplementedException();
            }

            public string TemplateFor(object model, string categoryOrHttpMethod = null)
            {
                throw new NotImplementedException();
            }

            public string TemplateFor<TModel>(params Func<object, object>[] hash) where TModel : class, new()
            {
                throw new NotImplementedException();
            }

            public string UrlFor(Type modelType, RouteParameters parameters, string categoryOrHttpMethod = null)
            {
                throw new NotImplementedException();
            }
        }
    }
}
