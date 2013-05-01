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
        void IExcelMapping.WriteTo(IExcelWriter writer, IDisplayFormatter formatter, IServiceLocator locator,
                                   object model)
        {
            var grid = locator.GetInstance(typeof (TGridType)).As<IGridDefinition>();
            var allColumns = grid.Columns();

            var fieldAccessService = locator.GetInstance<IFieldAccessService>();

            var rights =
                new Cache<IGridColumn, AccessRight>(col => fieldAccessService.RightsFor(null, col.Accessor.InnerProperty));

            var filteredColumns = allColumns.Where(col => !rights[col].Equals(AccessRight.None)).OrderByDescending(x => x.IsFrozen).ToList();
            var columnInfo = GetColumnInfo(filteredColumns);
            writer.WriteHeaders(columnInfo.Headers);

            var data = model.As<IDictionary<string, object>>();
            var rowData = data["data"].As<IEnumerable<IDictionary<string, object>>>();

            rowData.Each(row =>
            {
                var values = new List<string>();
                row.Each(column =>
                {
                    if (columnInfo.FieldNames.Contains(column.Key))
                    {
                        values.Add(column.Value.ToString());
                    }
                });
                writer.WriteRow(values.ToArray());
            });
        }

        private ColumnInfo GetColumnInfo(IEnumerable<IGridColumn> columns)
        {
            var headers = new List<string>();
            var fields = new List<string>();

            columns.Each(column =>
                column.Properties.Each((key, value) =>
                {
                    if (key == "name")
                    {
                        headers.Add(value.ToString());
                    }

                    if (key == "field")
                    {
                        fields.Add(value.ToString());
                    }
                }));

            return new ColumnInfo
            {
                Headers = headers.ToArray(),
                FieldNames = fields.ToArray()
            };
        }

        private class ColumnInfo
        {
            public string[] Headers { get; set; }
            public string[] FieldNames { get; set; }
        }
    }
}
