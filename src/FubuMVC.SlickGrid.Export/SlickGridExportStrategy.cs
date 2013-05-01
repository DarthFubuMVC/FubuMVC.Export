using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public class SlickGridExportStrategy<TGridType> : IApplyExcelWriterStrategy
        where TGridType : IGridDefinition
    {
        private static readonly Type OutputType = typeof (IDictionary<string, object>);
        private readonly Type _gridType = typeof (TGridType);
        private readonly Type _resourceType;

        public SlickGridExportStrategy()
        {
            _resourceType = _gridType.GetBaseType(type =>
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof (GridDefinition<>)).GetGenericArguments().FirstOrDefault();
        }

        public bool Matches(BehaviorChain chain)
        {
            return chain.Calls.Any(c =>
                c.HasOutput
                    && c.OutputType() == OutputType
                    && c.HandlerType.GetGenericArguments().First() == _resourceType);
        }

        public void Apply(BehaviorChain chain)
        {
            var mapping = typeof (SlickGridExcelMapping<,>).MakeGenericType(OutputType, _gridType);
            var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping, OutputType);
            chain.Output.Writers.AddToEnd(node);
        }
    }
}