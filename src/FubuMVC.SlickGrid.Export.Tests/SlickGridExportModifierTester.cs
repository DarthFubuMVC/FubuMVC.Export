using System.Linq;
using FubuMVC.Export;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.SlickGrid.Export.Tests
{
    [TestFixture]
    public class SlickGridExportModifierTester : InteractionContext<SlickGridExportModifier>
    {
        protected override void beforeEach()
        {
            base.beforeEach();

            ExportGraph.Graph = new ExportGraph();
        }

        [TearDown]
        public void TearDown()
        {
            ExportGraph.Graph = null;
        }

        [Test]
        public void should_register_SlickGridExportStrategy_for_MyGrid()
        {
            var graph = ExportGraph.Graph;

            ClassUnderTest.Modify(graph);

            graph.Strategies().OfType<SlickGridExportStrategy<MyGrid>>().Count().ShouldEqual(1);
        }

        [Test]
        public void should_NOT_register_SlickGridExportStrategy_for_MyNonEnabledGrid()
        {
            var graph = ExportGraph.Graph;

            ClassUnderTest.Modify(graph);

            graph.Strategies().OfType<SlickGridExportStrategy<MyNonEnabledGrid>>().Count().ShouldEqual(0);
        }
    }

    public class MyPerson
    {
        public string Name { get; set; }
    }

    [ExcelExport]
    public class MyGrid : GridDefinition<MyPerson>
    {
        public MyGrid()
        {
            Column(c => c.Name);
            this.EnableExcelExport();
        }
    }

    public class MyNonEnabledGrid : GridDefinition<MyPerson>
    {
        public MyNonEnabledGrid()
        {
            Column(c => c.Name);
            this.EnableExcelExport();
        }
    }
}
