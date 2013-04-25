using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class ApplySlickGridExcelWriters : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<SlickGridExcelExportSettings>();

            var outputType = typeof (IDictionary<string, object>);

            graph.Behaviors
                .Each(behavior =>
                    behavior.Calls
                        .Where(c => c.HasOutput && c.OutputType() == outputType)
                        .Each(action =>
                        {
                            settings.GridDefinitions.Each(gridType =>
                            {
                                var resourceType = gridType.BaseType.GetGenericArguments()[0];
                                var handlerResourceType = action.HandlerType.GetGenericArguments()[0];
                                if (resourceType == handlerResourceType)
                                {
                                    var mapping = typeof (SlickGridExcelMapping<,>).MakeGenericType(outputType, gridType);
                                    var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping,
                                        outputType);
                                    behavior.Output.Writers.AddToEnd(node);
                                }
                            });
                        }));
        }
    }
}
