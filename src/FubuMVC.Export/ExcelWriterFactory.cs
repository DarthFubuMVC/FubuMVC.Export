using System;

namespace FubuMVC.Export
{
    public class ExcelWriterResponse : IDisposable
    {
        public IExcelWriter Writer { get; set; }
        public string FileName { get; set; }

        public void Dispose()
        {
            if (Writer != null)
            {
                Writer.Dispose();
            }
        }
    }

    public interface IExcelWriterFactory
    {
        ExcelWriterResponse Build();
    }

    public class ExcelWriterFactory<TWriter> : IExcelWriterFactory where TWriter : class, IExcelWriter, new()
    {
        private readonly ITemporaryFileStorage _temporaryFileStorage;

        public ExcelWriterFactory(ITemporaryFileStorage temporaryFileStorage)
        {
            _temporaryFileStorage = temporaryFileStorage;
        }

        public ExcelWriterResponse Build()
        {
            var fileName = _temporaryFileStorage.Create();

            var writer = new TWriter();
            writer.Stream(_temporaryFileStorage.Open(fileName));

            return new ExcelWriterResponse
            {
                FileName = fileName,
                Writer = writer
            };
        }
    }
}