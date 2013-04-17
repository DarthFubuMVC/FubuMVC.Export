using FubuMVC.Core;
using FubuMVC.StructureMap;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace FubuMVC.Export.Tests
{
    public class FubuTestApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            var container = BuildContainer();

            return FubuApplication
                .For<ExportTestRegistry>()
                .StructureMap(() => container);
        }

        public static IContainer BuildContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<CoreRegistry>();
            });

            return ObjectFactory.Container;
        }
    }

    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });
        }
    }
}
