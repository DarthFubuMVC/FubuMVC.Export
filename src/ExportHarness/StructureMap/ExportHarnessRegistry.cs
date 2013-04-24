using ExportHarness.Infrastructure.Conneg;
using ExportHarness.People;
using FubuMVC.Core;
using FubuMVC.SlickGrid.Export;

namespace ExportHarness
{
    public class ExportHarnessRegistry : FubuRegistry
    {
        public ExportHarnessRegistry()
        {
            Policies.Add<ApplyPersonHtmlExport>();

            AlterSettings<SlickGridExcelExportSettings>(settings =>
            {
                settings.GridDefinitions.Add(typeof (PeopleGrid));
            });
        }
    }
}