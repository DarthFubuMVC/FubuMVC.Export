using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class SimpleStrategySource : IExcelWriterStrategySource
    {
        private readonly IList<IApplyExcelWriterStrategy> _strategies = new List<IApplyExcelWriterStrategy>();

        public SimpleStrategySource With(IApplyExcelWriterStrategy strategy)
        {
            _strategies.Add(strategy);
            return this;
        }

        public IEnumerable<IApplyExcelWriterStrategy> FindStrategies()
        {
            return _strategies;
        }
    }
}
