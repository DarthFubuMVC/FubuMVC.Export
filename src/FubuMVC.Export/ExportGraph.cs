using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class ExportGraph
    {
        private readonly IList<IApplyExcelWriterStrategy> _source = new List<IApplyExcelWriterStrategy>();

        public IEnumerable<IApplyExcelWriterStrategy> Strategies()
        {
            return _source;
        }

        public void AlterWith(IApplyExcelWriterStrategy strategy)
        {
            _source.Add(strategy);
        }

        public void AlterWith(IEnumerable<IApplyExcelWriterStrategy> strategies)
        {
            _source.AddRange(strategies);
        }

        public static ExportGraph Graph { get; set; }

        public static ExportGraph BuildFrom(FubuExportSettings settings)
        {
            Graph = new ExportGraph();
            settings.Modifiers().Each(m => m.Modify(Graph));

            var graph = Graph;
            Graph = null;

            return graph;
        }
    }
}