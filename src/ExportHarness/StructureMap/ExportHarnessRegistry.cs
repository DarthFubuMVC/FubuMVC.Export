using ExportHarness.Infrastructure.Conneg;
using FubuMVC.Core;

namespace ExportHarness
{
    public class ExportHarnessRegistry : FubuRegistry
    {
        public ExportHarnessRegistry()
        {
            Policies.Add<ApplyPersonHtmlExport>();
        }
    }
}