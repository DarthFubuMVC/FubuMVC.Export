using System.Collections.Generic;
using System.IO;

namespace FubuMVC.Export.Tests
{
    public class RecordingExcelWriter : IExcelWriter
    {
        private readonly IList<string> _headers = new List<string>();
        private readonly IList<string> _rows = new List<string>();

        public void Stream(Stream stream)
        {
        }

        public void WriteHeaders(string[] headers)
        {
            _headers.AddRange(headers);
        }

        public void WriteRow(string[] values)
        {
            _rows.Add(values.Join(","));
        }

        public IList<string> Headers()
        {
            return _headers;
        }

        public IList<string> Rows()
        {
            return _rows;
        }

        public void Dispose()
        {
        }
    }
}