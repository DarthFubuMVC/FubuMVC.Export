using System;
using FubuCore;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;

namespace FubuMVC.Export
{
    public class ApplyResourceExcelWriterStrategy : IApplyExcelWriterStrategy
    {
        private readonly Type _resourceType;
        private readonly Type _mappingType;

        public ApplyResourceExcelWriterStrategy(Type resourceType, Type mappingType)
        {
            _resourceType = resourceType;
            _mappingType = mappingType;
        }

        public bool Matches(BehaviorChain chain)
        {
            return chain.HasResourceType() && chain.ResourceType() == _resourceType;
        }

        public void Apply(BehaviorChain chain)
        {
            var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) _mappingType, _resourceType);
            chain.Output.Writers.InsertFirst(node);
        }
    }
}