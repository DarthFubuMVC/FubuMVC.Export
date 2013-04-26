using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core.Registration;

namespace FubuMVC.Export
{
    public class ExcelWriterStrategySource : IExcelWriterStrategySource
    {
        private static readonly Lazy<IEnumerable<Type>> Mappings;
        private static readonly Lazy<IDictionary<Type, IEnumerable<Type>>> Dictionary;

        static ExcelWriterStrategySource()
        {
            Mappings = new Lazy<IEnumerable<Type>>(findMappings);
            Dictionary = new Lazy<IDictionary<Type, IEnumerable<Type>>>(buildDictionary);
        }

        public IEnumerable<IApplyExcelWriterStrategy> FindStrategies()
        {
            var strategies = new List<IApplyExcelWriterStrategy>();

            Mappings.Value.Each(mapping =>
            {
                var dictionaryEntry = Dictionary.Value.First(x => x.Value.Contains(mapping));
                strategies.Add(new ApplyResourceExcelWriterStrategy(dictionaryEntry.Key, mapping));
            });

            return strategies;
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