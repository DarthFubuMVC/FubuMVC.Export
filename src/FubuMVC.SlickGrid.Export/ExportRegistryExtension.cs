using FubuMVC.Core;

namespace FubuMVC.SlickGrid.Export
{
    public class ExportRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Policies.Add<ApplySlickGridExcelWriters>();
        }
    }
}
