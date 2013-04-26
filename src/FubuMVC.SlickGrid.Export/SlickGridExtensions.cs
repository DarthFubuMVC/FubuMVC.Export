using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public static class SlickGridExtensions
    {
        public static void EnableExcelExport<T>(this T grid) where T : IGridDefinition
        {
            EnableExcelExport(grid, new SlickGridExportStrategy<T>());
        }

        public static void EnableExcelExport<T>(this T grid, IApplyExcelWriterStrategy strategy) where T : IGridDefinition
        {
            if (ExportGraph.Graph != null)
            {
                ExportGraph.Graph.AlterWith(strategy);
            }
        }
    }
}