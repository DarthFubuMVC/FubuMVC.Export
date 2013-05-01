using FubuMVC.Core;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public class ApplySlickGridExcelWriters : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.AlterSettings<FubuExportSettings>(settings =>
            {
                settings.ModifyWith(new SlickGridExportModifier());
            });
        }
    }
}
