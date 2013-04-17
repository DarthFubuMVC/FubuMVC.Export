using System;
using FubuCore;
using FubuCore.Csv;
using FubuCore.Formatting;

namespace FubuMVC.Export
{
    public interface IExcelMapping : IColumnMapping
    {
        Type SourceType();
        void WriteTo(IExcelWriter writer, IDisplayFormatter formatter, IServiceLocator locator, object model);
    }
}
