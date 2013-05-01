using FubuMVC.Core;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export.Tests
{
    public class ExportTestRegistry : FubuRegistry
    {
        public ExportTestRegistry()
        {
            Import<ExportRegistryExtension>();
            Import<ApplySlickGridExcelWriters>();
            Actions.IncludeClassesSuffixedWithEndpoint();
        }
    }
}
