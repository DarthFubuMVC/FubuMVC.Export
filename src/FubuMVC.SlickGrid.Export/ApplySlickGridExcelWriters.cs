using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public static class SlickGridExtensions
    {
        public static void EnableExcelExport<T>(this T grid) where T : IGridDefinition
        {
            if (ExportGraph.Graph != null)
            {
                ExportGraph.Graph.AlterWith(new SlickGridExportStrategy<T>());
            }
        }
    }

    public class ExcelExportAttribute : Attribute
    {
    }

    public class SlickGridExportModifier : IExportGraphModifier
    {
        private static readonly Lazy<IEnumerable<Type>> GridDefinitions = new Lazy<IEnumerable<Type>>();

        static SlickGridExportModifier()
        {
            GridDefinitions = new Lazy<IEnumerable<Type>>(findExcelExportAttribues);
        }

        public void Modify(ExportGraph graph)
        {
            // create an instance of each grid definition to kick off the call to ExportGraph
            GridDefinitions.Value.Each(x => Activator.CreateInstance(x));
        }

        private static IEnumerable<Type> findExcelExportAttribues()
        {
            var types = TypePool.AppDomainTypes();
            var mappings = types.TypesMatching(type => type.HasAttribute<ExcelExportAttribute>());
            return mappings;
        }
    }

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
