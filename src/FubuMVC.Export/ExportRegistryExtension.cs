using FubuMVC.Core;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Export.Endpoints;

namespace FubuMVC.Export
{
    public class ExportRegistryExtension : IFubuRegistryExtension
    {
        public void Configure(FubuRegistry registry)
        {
            registry.Services(s =>
            {
                s.SetServiceIfNone<ITemporaryFileStorage, TemporaryFileStorage>();
                s.SetServiceIfNone<IExcelWriterFactory, ExcelWriterFactory<XlsxExcelWriter>>();
                s.SetServiceIfNone<IExcelMappingRunner, ExcelMappingRunner>();
            });

            registry.Policies.Add<ApplyExcelWriters>();
            registry.Policies.Add<DownloadFileConvention>();

            registry.Actions.FindWith<DownloadExportFileActionSource>();

            registry.AlterSettings<FubuExportSettings>(settings =>
            {
                settings.FindWith(new ExcelWriterStrategySource());
            });
        }
    }
}
