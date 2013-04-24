using System;
using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;

namespace FubuMVC.Export
{
    [ConfigurationType(ConfigurationType.Conneg)]
    public class ApplyExcelWriters : IConfigurationAction
    {
        private static readonly Lazy<IEnumerable<Type>> Mappings;

        static ApplyExcelWriters()
        {
            Mappings = new Lazy<IEnumerable<Type>>(findMappings);
        }

        public void Configure(BehaviorGraph graph)
        {
            graph
                .Behaviors
                .Each(chain =>
                {
                    Mappings.Value.Each(mapping =>
                    {
                        var type = mapping.BaseType.GetGenericArguments()[0];

                        if (chain.ResourceType() == type)
                        {
                            var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping,
                                                                                                   type);

                            chain.Output.Writers.InsertFirst(node);
                        }
                    });
                });
        }

        private static IEnumerable<Type> findMappings()
        {
            var types = TypePool.AppDomainTypes();

            var mappings = types.TypesMatching(type => type.Closes(typeof (ExcelMapping<,>)));
            return mappings;
        }
    }
}