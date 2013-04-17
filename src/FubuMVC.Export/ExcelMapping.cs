using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FubuCore;
using FubuCore.Csv;
using FubuCore.Formatting;

namespace FubuMVC.Export
{
    public abstract class ExcelMapping<TSource>
        : ExcelMapping<TSource, TSource>
        where TSource : class 
    {
    }

    public abstract class ExcelMapping<TSource, TDestination>
        : ColumnMapping<TDestination>, IExcelMapping
        where TSource : class
        where TDestination : class
    {
        private Type _sourceType;
        private MethodInfo _rowsMethod;

        public void Source<TSourceType>()
        {
            _sourceType = typeof (TSourceType);

            var templateType = _sourceType.FindInterfaceThatCloses(typeof (IExcelSource<>));
            if (templateType != null)
            {
                if (templateType.GetGenericArguments().First() != typeof(TSource))
                {
                    throw new ArgumentOutOfRangeException("Wrong type as the argument to IExcelSource<>");
                }

                return;
            }

            templateType = _sourceType.FindInterfaceThatCloses(typeof(IExcelSource<,>));
            if (templateType != null)
            {
                if (templateType.GetGenericArguments().First() != typeof(TSource))
                {
                    throw new ArgumentOutOfRangeException("Wrong type as the argument to IExcelSource<,>");
                }


                return;
            }

            throw new ArgumentOutOfRangeException("TSourceType must be either IExcelSource<T> or IExcelSource<T, K>");
        }

        Type IExcelMapping.SourceType()
        {
            return _sourceType;
        }

        void IExcelMapping.WriteTo(IExcelWriter writer, IDisplayFormatter formatter, IServiceLocator locator, object model)
        {
            var columnMapping = this.As<IColumnMapping>();
            writer.WriteHeaders(columnMapping.Columns().Select(c => c.Name).ToArray());

            var rows = GetRows(model, locator);
            rows.Each(row =>
            {
                var values = new List<string>();
                columnMapping.Columns().Each(c =>
                {
                    values.Add(formatter.GetDisplayForProperty(c.Accessor, row));
                });

                writer.WriteRow(values.ToArray());
            });
        }

        private IEnumerable GetRows(object model, IServiceLocator locator)
        {
            var source = locator.GetInstance(_sourceType);
            if (_rowsMethod == null)
            {
                _rowsMethod = _sourceType.GetMethod("Rows");
            }
            return _rowsMethod.Invoke(source, new object[] { model }) as IEnumerable;
        }
    }
}