using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class ExcelMappingTester
    {
        [Test]
        public void should_throw_when_unknown_source()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MyInvalidMapper();
            });
        }

        [Test]
        public void should_throw_when_source_generic_single_arguments_dont_match()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MySwappedSingleSourceMapper();
            });
        }

        [Test]
        public void should_throw_when_source_generic_double_arguments_dont_match()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MySwappedDoubleSourceMapper();
            });
        }

        [Test]
        public void should_register_singlesource()
        {
            Assert.DoesNotThrow(() =>
            {
                new MySingleSourceMapper();
            });
        }

        [Test]
        public void should_register_doubleource()
        {
            Assert.DoesNotThrow(() =>
            {
                new MyDoubleSourceMapper();
            });
        }

        public class MyModel
        {
        }

        public class MyOtherModel
        {
        }

        public class InvalidSource
        {
        }

        public class SingleSource : IExcelSource<MyModel>
        {
            public IEnumerable<MyModel> Rows(MyModel model)
            {
                throw new NotImplementedException();
            }
        }

        public class DoubleSource : IExcelSource<MyModel, MyOtherModel>
        {
            public IEnumerable<MyOtherModel> Rows(MyModel model)
            {
                throw new NotImplementedException();
            }
        }

        public class SwappedSingleSource : IExcelSource<MyOtherModel>
        {
            public IEnumerable<MyOtherModel> Rows(MyOtherModel model)
            {
                throw new NotImplementedException();
            }
        }

        public class SwappedDoubleSource : IExcelSource<MyOtherModel, MyModel>
        {
            public IEnumerable<MyModel> Rows(MyOtherModel model)
            {
                throw new NotImplementedException();
            }
        }

        public class MySingleSourceMapper : ExcelMapping<MyModel>
        {
            public MySingleSourceMapper()
            {
                Source<SingleSource>();
            }
        }

        public class MyDoubleSourceMapper : ExcelMapping<MyModel, MyOtherModel>
        {
            public MyDoubleSourceMapper()
            {
                Source<DoubleSource>();
            }
        }

        public class MyInvalidMapper : ExcelMapping<MyModel>
        {
            public MyInvalidMapper()
            {
                Source<InvalidSource>();
            }
        }

        public class MySwappedSingleSourceMapper : ExcelMapping<MyModel>
        {
            public MySwappedSingleSourceMapper()
            {
                Source<SwappedSingleSource>();
            }
        }

        public class MySwappedDoubleSourceMapper : ExcelMapping<MyModel, MyOtherModel>
        {
            public MySwappedDoubleSourceMapper()
            {
                Source<SwappedDoubleSource>();
            }
        }
    }
}
