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
        private static readonly Type GridType = typeof (TGridType);
        private static readonly Type ResourceType = GridType.BaseType.GetGenericArguments()[0];

        public bool Matches(BehaviorChain chain)
        {
            return chain.Calls.Any(c =>
                c.HasOutput
                    && c.OutputType() == OutputType
                    && c.HandlerType.GetGenericArguments()[0] == ResourceType);
        }

        public void Apply(BehaviorChain chain)
        {
            var mapping = typeof (SlickGridExcelMapping<,>).MakeGenericType(OutputType, GridType);
            var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping, OutputType);
            chain.Output.Writers.AddToEnd(node);
        }
    }
}