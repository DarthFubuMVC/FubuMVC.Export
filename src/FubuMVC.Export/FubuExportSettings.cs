using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Export
{
    public class FubuExportSettings
    {
        private readonly IList<IExcelWriterStrategySource> _sources = new List<IExcelWriterStrategySource>();
        private readonly IList<IExportGraphModifier> _modifiers = new List<IExportGraphModifier>();

        public IEnumerable<IExcelWriterStrategySource> Sources
        {
            get { return _sources; }
        }

        public IEnumerable<IExportGraphModifier> Modifiers()
        {
            return _modifiers;
        }

        public IEnumerable<IApplyExcelWriterStrategy> AllStrategies()
        {
            return Sources.SelectMany(s => s.FindStrategies());
        }

        public void ModifyWith(IExportGraphModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void FindWith(IExcelWriterStrategySource strategySource)
        {
            _sources.Add(strategySource);
        }
    }
}