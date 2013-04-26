using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public static class SlickGridExtensions
    {
        public static void EnableExcelExport<T>(this T grid) where T : IGridDefinition
        {
            if (ExportGraph.Graph != null)
            {
                ExportGraph.Graph.AlterWith(new SlickGridExportStrategy<T>());
            }
        }
    }
}