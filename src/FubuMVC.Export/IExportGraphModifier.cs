using FubuMVC.Core.Registration;

namespace FubuMVC.Export
{
    public interface IExportGraphModifier
    {
        void Modify(ExportGraph graph);
    }
}