using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Export
{
    public class SimpleExportGraphModifier : IExportGraphModifier
    {
        private readonly IList<IExcelWriterStrategySource> _sources = new List<IExcelWriterStrategySource>();

        public void Modify(ExportGraph graph)
        {
            var strategies = _sources.SelectMany(x => x.FindStrategies());

            graph.AlterWith(strategies);
        }

        public SimpleExportGraphModifier FindWith(IExcelWriterStrategySource source)
        {
            _sources.Add(source);
            return this;
        }
    }
}