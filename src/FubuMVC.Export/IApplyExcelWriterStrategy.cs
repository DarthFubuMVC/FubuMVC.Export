using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Export
{
    public interface IApplyExcelWriterStrategy
    {
        bool Matches(BehaviorChain chain);
        void Apply(BehaviorChain chain);
    }
}