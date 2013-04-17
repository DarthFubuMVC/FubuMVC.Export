using FubuMVC.Core;

namespace FubuMVC.Export.Tests
{
    public class ExportTestRegistry : FubuRegistry
    {
        public ExportTestRegistry()
        {
            Import<ExportRegistryExtension>();
            Actions.IncludeClassesSuffixedWithEndpoint();
        }
    }
}
