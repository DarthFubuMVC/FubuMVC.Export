using System;
using FubuMVC.Core.Http;
using NUnit.Framework;
using StructureMap;

namespace FubuMVC.Export.Tests
{
    public class IntegrationBootstrapper
    {
        private static readonly Lazy<IContainer> _container = new Lazy<IContainer>(bootstrapContainer);

        public static IContainer Container
        {
            get { return _container.Value; }
        }

        public static T GetConfiguredInstance<T>()
        {
            return _container.Value.GetInstance<T>();
        }

        public static void CleanUp()
        {
            if (_container.IsValueCreated)
            {
            }
        }

        private static IContainer bootstrapContainer()
        {
            new FubuTestApplication()
                .BuildApplication()
                .Bootstrap();

            ObjectFactory.Container.Configure(x =>
            {
                x.For<ICurrentHttpRequest>().Use(new StubCurrentHttpRequest());
            });

            return ObjectFactory.Container;
        }
    }

    [SetUpFixture]
    public class Shutdown
    {
        [TearDown]
        public void Cleanup()
        {
            IntegrationBootstrapper.CleanUp();
        }
    }
}