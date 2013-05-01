using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class SimpleStrategySourceTester
    {
        private SimpleStrategySource theSource;

        [SetUp]
        public void Setup()
        {
            theSource = new SimpleStrategySource();
        }

        [Test]
        public void should_return_registered_strategy()
        {
            var strategy = new MyStrategy();
            theSource
                .With(strategy)
                .FindStrategies()
                .Contains(strategy)
                .ShouldBeTrue();
        }

        public class MyStrategy : IApplyExcelWriterStrategy
        {
            public bool Matches(BehaviorChain chain)
            {
                throw new NotImplementedException();
            }

            public void Apply(BehaviorChain chain)
            {
                throw new NotImplementedException();
            }
        }
    }
}
