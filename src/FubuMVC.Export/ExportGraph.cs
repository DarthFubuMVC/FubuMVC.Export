using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class ExportGraph
    {
        private readonly IList<IApplyExcelWriterStrategy> _strategies = new List<IApplyExcelWriterStrategy>();

        static ExportGraph()
        {
            Graph = new ExportGraph();
        }

        public IExcelWriterStrategySource Source()
        {
            return new SimpleStrategySource(_strategies);
        }

        public void AlterWith(IApplyExcelWriterStrategy strategy)
        {
            _strategies.Add(strategy);
        }

        public static ExportGraph Graph { get; set; }
    }
}