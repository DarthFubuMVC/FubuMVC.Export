using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using FubuCore;
using FubuCore.Util;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;

namespace FubuMVC.Export
{
    [ConfigurationType(ConfigurationType.Conneg)]
    public class ApplyExcelWriters : IConfigurationAction
    {
        private static readonly Lazy<IEnumerable<Type>> Mappings;
        private static readonly Lazy<IDictionary<Type, IEnumerable<Type>>> Dictionary;

        static ApplyExcelWriters()
        {
            Mappings = new Lazy<IEnumerable<Type>>(findMappings);
            Dictionary = new Lazy<IDictionary<Type, IEnumerable<Type>>>(buildDictionary);
        }

        public void Configure(BehaviorGraph graph)
        {
            graph
                .Behaviors
                .Where(b => b.HasResourceType())
                .Each(chain =>
                {
                    var resourceType = chain.ResourceType();
                    if (Dictionary.Value.ContainsKey(resourceType))
                    {
                        var mappingType = Dictionary.Value[resourceType].FirstOrDefault();
                        var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mappingType,
                            resourceType);
                        chain.Output.Writers.InsertFirst(node);
                    }
                });
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
            var mappings = types.TypesMatching(type => type.Closes(typeof (ExcelMapping<,>)));
            return mappings;
        }
    }
}