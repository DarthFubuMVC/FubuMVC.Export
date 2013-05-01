using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Export;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.SlickGrid.Export.Tests
{
    [TestFixture]
    public class ApplySlickGridExcelWritersTester
    {
        private BehaviorGraph theGraph;

        [SetUp]
        public void Setup()
        {
            theGraph = BehaviorGraph.BuildFrom<ExportTestRegistry>();
        }

        [Test]
        public void should_register_register_the_slickgrid_modifier()
        {
            var settings = theGraph.Settings.Get<FubuExportSettings>();
            settings.Modifiers().OfType<SlickGridExportModifier>().Count().ShouldEqual(1);
        }
    }
}
