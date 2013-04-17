using System.Collections.Generic;
using FubuCore;
using FubuCore.Formatting;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class IntegratedExcelMappingTester
    {
        private RecordingExcelWriter theWriter;
        private MyExcelMapping theMapping;
        private InMemoryServiceLocator theLocator;
        private DisplayFormatter theFormatter;

        [SetUp]
        public void Setup()
        {
            theWriter = new RecordingExcelWriter();
            theMapping = new MyExcelMapping();
            theLocator = new InMemoryServiceLocator();
            theFormatter = new DisplayFormatter(theLocator, new Stringifier());

            theLocator.Add(new MyExcelSource());

            theMapping.As<IExcelMapping>().WriteTo(theWriter, theFormatter, theLocator, null);
        }

        [Test]
        public void writes_headers()
        {
            theWriter.Headers().ShouldHaveCount(2);
        }

        [Test]
        public void writes_rows()
        {
            theWriter.Rows().ShouldHaveCount(3);
        }

        [Test]
        public void writes_multiple_columns()
        {
            theWriter.Rows().ShouldHaveCount(3);
            theWriter.Rows()[0].ShouldEqual("Fred,XXX-XX-1234");
            theWriter.Rows()[1].ShouldEqual("George,XXX-XX-5678");
            theWriter.Rows()[2].ShouldEqual("Amy,XXX-XX-9012");
        }

        [Test]
        public void ignores_properties_that_are_not_defined_as_columns()
        {
            theWriter.Headers().Each(h => h.ShouldNotEqual("Ignored"));
        }

        [Test]
        public void should_write_the_headers_in_the_order_in_which_they_are_defined()
        {
            theWriter.Headers().Join(",").ShouldEqual("person_name,SSN");
        }

        [Test]
        public void should_write_the_columns_in_the_order_in_which_they_are_defined()
        {
            theWriter.Rows()[0].ShouldEqual("Fred,XXX-XX-1234");
        }

        [Test]
        public void should_use_alias_for_column_header_if_one_exists()
        {
            theWriter.Headers()[0].ShouldEqual("person_name");
        }

        [Test]
        public void should_set_the_sourcetype()
        {
            theMapping.As<IExcelMapping>().SourceType().ShouldEqual(typeof (MyExcelSource));
        }

        public class MyModel
        {
            public string Name { get; set; }
            public string SSN { get; set; }
            public bool Ignored { get; set; }
        }

        public class MyExcelMapping : ExcelMapping<MyModel>
        {
            public MyExcelMapping()
            {
                Column(c => c.Name).Alias("person_name");
                Column(c => c.SSN);
                Source<MyExcelSource>();
            }
        }

        public class MyExcelSource : IExcelSource<MyModel>
        {
            public IEnumerable<MyModel> Rows(MyModel model)
            {
                yield return new MyModel {Name = "Fred", SSN = "XXX-XX-1234"};
                yield return new MyModel {Name = "George", SSN = "XXX-XX-5678"};
                yield return new MyModel {Name = "Amy", SSN = "XXX-XX-9012"};
            }
        }
    }
}