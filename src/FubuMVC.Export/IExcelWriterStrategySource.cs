using System.Collections.Generic;

namespace FubuMVC.Export
{
    public interface IExcelWriterStrategySource
    {
        IEnumerable<IApplyExcelWriterStrategy> FindStrategies();
    }
}