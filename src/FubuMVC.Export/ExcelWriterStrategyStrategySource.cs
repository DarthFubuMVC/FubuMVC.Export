using System.Collections.Generic;

namespace FubuMVC.Export
{
    public class ExcelWriterStrategyStrategySource : IExcelWriterStrategySource
    {
        public IEnumerable<IApplyExcelWriterStrategy> FindStrategies()
        {
            yield return new ApplyResourceExcelWriterStrategy();
        }
    }
}