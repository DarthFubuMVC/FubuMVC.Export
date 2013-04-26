using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Resources.Conneg;

namespace FubuMVC.Export
{
    public class ApplyResourceExcelWriterStrategy : IApplyExcelWriterStrategy
    {
        private static readonly Lazy<IEnumerable<Type>> Mappings;
        private static readonly Lazy<IDictionary<Type, IEnumerable<Type>>> Dictionary;

        static ApplyResourceExcelWriterStrategy()
        {
            Mappings = new Lazy<IEnumerable<Type>>(findMappings);
            Dictionary = new Lazy<IDictionary<Type, IEnumerable<Type>>>(buildDictionary);
        }

        public bool Matches(BehaviorChain chain)
        {
            return chain.HasResourceType()
                && Dictionary.Value.ContainsKey(chain.ResourceType());
        }

        public void Apply(BehaviorChain chain)
        {
            var resourceType = chain.ResourceType();
            var mappingType = Dictionary.Value[resourceType].First();
            var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mappingType,
                resourceType);
            chain.Output.Writers.InsertFirst(node);
        }

        private static IDictionary<Type, IEnumerable<Type>> buildDictionary()
        {
            return Mappings.Value
                .GroupBy(x => x.BaseType.GetGenericArguments()[0])
                .Select(x => new {ResourceType = x.Key, MappingTypes = x.ToList()})
                .ToDictionary(x => x.ResourceType, x => (IEnumerable<Type>) x.MappingTypes);
        }

        private static IEnumerable<Type> findMappings()
        {
            var types = TypePool.AppDomainTypes();
            return types.TypesMatching(type => type.Closes(typeof (ExcelMapping<,>)));
        }
    }
}