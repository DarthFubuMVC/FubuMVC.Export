using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuCore.Formatting;
using FubuCore.Util;
using FubuMVC.Core.UI.Security;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public class SlickGridExcelMapping<TResource, TGridType>
        : ExcelMapping<TResource>, IExcelMapping
        where TResource : class
        where TGridType : IGridDefinition
    {
        //private Type _gridType;

        //public void SlickGrid<TGridType>()
        //{
        //    _gridType = typeof (TGridType);
        //}

        void IExcelMapping.WriteTo(IExcelWriter writer, IDisplayFormatter formatter, IServiceLocator locator,
                                   object model)
        {
            var grid = locator.GetInstance(typeof (TGridType)).As<IGridDefinition>();
            var allColumns = grid.Columns();

            var fieldAccessService = locator.GetInstance<IFieldAccessService>();

            var rights =
                new Cache<IGridColumn, AccessRight>(col => fieldAccessService.RightsFor(null, col.Accessor.InnerProperty));

            var filteredColumns = allColumns.Where(col => !rights[col].Equals(AccessRight.None)).OrderByDescending(x => x.IsFrozen).ToList();
            var headers = GetHeaders(filteredColumns, rights);
            writer.WriteHeaders(headers.Headers);

            var runnerType = grid.DetermineRunnerType();
            var data = model.As<IDictionary<string, object>>();
            var rowData = data["data"].As<IEnumerable<IDictionary<string, object>>>();

            rowData.Each(row =>
            {
                var values = new List<string>();
                row.Each(column =>
                {
                    if (headers.FieldNames.Contains(column.Key))
                    {
                        values.Add(column.Value.ToString());
                    }
                });
                writer.WriteRow(values.ToArray());
            });
        }


        private HeaderInfo GetHeaders(IList<IGridColumn> columns, Cache<IGridColumn, AccessRight> rights)
        {
            var headers = new List<string>();
            var fields = new List<string>();

            for (var i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                var access = rights[column];
                column.Properties
                      .Each((key, value) =>
                      {
                          if (key == "editor" && !AccessRight.All.Equals(access))
                          {
                              return;
                          }

                          if (key == "name")
                          {
                              headers.Add(value.ToString());
                          }

                          if (key == "field")
                          {
                              fields.Add(value.ToString());
                          }
                      });
            }

            return new HeaderInfo
            {
                Headers = headers.ToArray(),
                FieldNames = fields.ToArray()
            };
        }

        private IDictionary<string, object> GetData(IServiceLocator locator, Type runnerType)
        {
            var runner = locator.GetInstance(runnerType);
            var method = runnerType.GetMethod("Run");
            return method.Invoke(runner, null) as IDictionary<string, object>;
        }

        private class HeaderInfo
        {
            public string[] Headers { get; set; }
            public string[] FieldNames { get; set; }
        }
    }
}
