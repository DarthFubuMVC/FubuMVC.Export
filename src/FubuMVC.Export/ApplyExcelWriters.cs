using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Registration;

namespace FubuMVC.Export
{
    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class ApplyExcelWriters : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<FubuExportSettings>();
            var exportGraph = ExportGraph.Graph;

            // hook in any additional configuration
            settings.Modifiers().Each(x => x.Modify(exportGraph));

            // add configured source
            settings.FindWith(exportGraph.Source());

            // don't need the graph any longer
            ExportGraph.Graph = null;

            var strategies = settings.AllStrategies();

            graph
                .Behaviors
                .Where(x => strategies.Any(s => s.Matches(x)))
                .Each(chain => strategies.Where(s => s.Matches(chain)).Each(s => s.Apply(chain)));
        }
    }

}