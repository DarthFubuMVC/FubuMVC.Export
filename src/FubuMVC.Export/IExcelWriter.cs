using System;
using System.IO;

namespace FubuMVC.Export
{
    public interface IExcelWriter : IDisposable
    {
        void Stream(Stream stream);
        void WriteHeaders(string[] headers);
        void WriteRow(string[] values);
    }
}