using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class SimpleStrategySource : IExcelWriterStrategySource
    {
        private readonly IEnumerable<IApplyExcelWriterStrategy> _strategies;

        public SimpleStrategySource(IEnumerable<IApplyExcelWriterStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IEnumerable<IApplyExcelWriterStrategy> FindStrategies()
        {
            return _strategies;
        }
    }
}
