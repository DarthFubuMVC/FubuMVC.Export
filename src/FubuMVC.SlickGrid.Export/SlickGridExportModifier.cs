using System;
using System.Collections.Generic;
using FubuCore.Reflection;
using FubuMVC.Core.Registration;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public class SlickGridExportModifier : IExportGraphModifier
    {
        private static readonly Lazy<IEnumerable<Type>> GridDefinitions = new Lazy<IEnumerable<Type>>();

        static SlickGridExportModifier()
        {
            GridDefinitions = new Lazy<IEnumerable<Type>>(findExcelExportAttribues);
        }

        public void Modify(ExportGraph graph)
        {
            // create an instance of each grid definition to kick off the call to ExportGraph
            GridDefinitions.Value.Each(x => Activator.CreateInstance(x));
        }

        private static IEnumerable<Type> findExcelExportAttribues()
        {
            var types = TypePool.AppDomainTypes();
            var mappings = types.TypesMatching(type => type.HasAttribute<ExcelExportAttribute>());
            return mappings;
        }
    }
}