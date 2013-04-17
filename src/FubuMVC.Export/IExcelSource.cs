using System.Collections.Generic;

namespace FubuMVC.Export
{
    public interface IExcelSource<TSource>
    {
        IEnumerable<TSource> Rows(TSource model);
    }

    public interface IExcelSource<TSource, TDestination>
    {
        IEnumerable<TDestination> Rows(TSource model);
    }
}