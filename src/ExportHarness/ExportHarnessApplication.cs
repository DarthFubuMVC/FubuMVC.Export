using FubuMVC.Core;
using FubuMVC.StructureMap;

namespace ExportHarness
{
    public class ExportHarnessApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            var container = WebBootstrapper.BuildContainer();

            return FubuApplication
                .For<ExportHarnessRegistry>()
                .StructureMap(() => container);
        }
    }
}