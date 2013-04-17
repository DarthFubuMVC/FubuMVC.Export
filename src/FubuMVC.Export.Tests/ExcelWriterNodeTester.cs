using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.ObjectGraph;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class ExcelWriterNodeTester
    {
        private BehaviorGraph theGraph;

        [SetUp]
        public void Setup()
        {
            theGraph = BehaviorGraph.BuildFrom<ExportTestRegistry>();
        }

        [Test]
        public void should_register_ExcelWriterNode_for_endpoint_with_mapping()
        {
            var chain = theGraph.BehaviorFor<MyDownloadEndpoint>(x => x.get_download());
            var node = chain.Output.Writers.OfType<ExcelWriterNode<MyDownloadModel>>().FirstOrDefault();
            node.ShouldNotBeNull();
        }

        [Test]
        public void should_NOT_register_ExcelWriterNode_for_endpoint_without_mapping()
        {
            var chain = theGraph.BehaviorFor<MyNonDownloadEndpoint>(x => x.get_model());
            var node = chain.Output.Writers.OfType<ExcelWriterNode<MyNonDownloadModel>>().FirstOrDefault();
            node.ShouldBeNull();
        }

        [Test]
        public void should_register_IExcelMapping_dependency()
        {
            var node = new ExcelWriterNode<MyDownloadModel>(typeof (MyExcelMapping));
            var objDef = ((IContainerModel) node).ToObjectDef();
            var dependency = objDef.Dependencies.ToArray()[1] as ConfiguredDependency;
            var mappedDependency = dependency.Definition.Dependencies.First();
            mappedDependency.DependencyType.ShouldEqual(typeof (IExcelMapping));
        }

        [Test]
        public void should_return_the_generic_type_as_the_resourcetype()
        {
            var node = new ExcelWriterNode<MyDownloadModel>(typeof (MyExcelMapping));
            node.ResourceType.ShouldEqual(typeof (MyDownloadModel));
        }

        [Test]
        public void should_return_the_given_mappingtype_as_the_mappingtype()
        {
            var node = new ExcelWriterNode<MyDownloadModel>(typeof (MyExcelMapping));
            node.MappingType.ShouldEqual(typeof (MyExcelMapping));
        }

        [Test]
        public void should_return_the_proper_mimetypes()
        {
            var node = new ExcelWriterNode<MyDownloadModel>(typeof (MyExcelMapping));
            node.Mimetypes.Join("").ShouldEqual("application/xlsx");
        }

        public class MyDownloadModel
        {
            public string Name { get; set; }
        }

        public class MyDownloadEndpoint
        {
            public MyDownloadModel get_download()
            {
                return new MyDownloadModel {Name = "Jaime"};
            }
        }

        public class MyExcelMapping : ExcelMapping<MyDownloadModel>
        {
            public MyExcelMapping()
            {
                Column(c => c.Name);
                Source<MyExcelSource>();
            }
        }

        public class MyExcelSource : IExcelSource<MyDownloadModel>
        {
            public IEnumerable<MyDownloadModel> Rows(MyDownloadModel model)
            {
                yield return new MyDownloadModel {Name = "Fred"};
                yield return new MyDownloadModel {Name = "Jaime"};
            }
        }

        public class MyNonDownloadModel
        {
        }

        public class MyNonDownloadEndpoint
        {
            public MyNonDownloadEndpoint get_model()
            {
                return new MyNonDownloadEndpoint();
            }
        }
    }
}