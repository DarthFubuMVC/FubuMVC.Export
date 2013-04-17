using System.Collections.Generic;
using System.Reflection;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Export.Endpoints
{
    [ConfigurationType(ConfigurationType.Discovery)]
    public class DownloadExportFileActionSource : IActionSource
    {
        public IEnumerable<ActionCall> FindActions(Assembly applicationAssembly)
        {
            yield return ActionCall.For<DownloadExportFileEndpoint>(x => x.get_export_download_Hash(null));
        }
    }
}
