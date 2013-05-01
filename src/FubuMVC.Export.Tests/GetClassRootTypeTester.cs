using FubuCore.Csv;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class GetClassRootTypeTester
    {
        [Test]
        public void should_work_with_single_depth_inheritance()
        {
            var root = typeof (SingleDepthGrid).GetBaseType();

            root.ShouldBeTheSameAs(typeof (ColumnMapping<PersonObject>));
        }

        [Test]
        public void should_work_with_multi_depth_inheritance()
        {
            var root = typeof (MultiDepthGrid).GetBaseType();
            root.ShouldBeTheSameAs(typeof (ColumnMapping<PersonObject>));
        }

        [Test]
        public void should_work_with_multi_depth_inheritance_as_generic()
        {
            var root = ExportTypeExtensions.GetBaseType<MultiDepthGrid>();

            root.ShouldBeTheSameAs(typeof (ColumnMapping<PersonObject>));
        }
    }

    public class PersonObject
    {
    }

    public class SingleDepthGrid : ColumnMapping<PersonObject>
    {
    }

    public class MultiDepthGrid : SingleDepthGrid
    {
    }
}
