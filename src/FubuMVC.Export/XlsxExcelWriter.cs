using System;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace FubuMVC.Export
{
    public class XlsxExcelWriter : IExcelWriter
    {
        #region _EmptyXlsx

        private const string EmptyXlsx = @"UEsDBBQABgAIAAAAIQBi7p1oYQEAAJAEAAATAAgCW0NvbnRlbnRfVHlwZXNdLnhtbCCiBAIooAAC
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAACs
lE1PwzAMhu9I/IcqV9Rm44AQWrcDH0eYxPgBoXHXaGkSxd7Y/j1u9iGEyqaJXRq1sd/3iWtnNFm3
NltBRONdKYbFQGTgKq+Nm5fiY/aS34sMSTmtrHdQig2gmIyvr0azTQDMONthKRqi8CAlVg20Cgsf
wPFO7WOriF/jXAZVLdQc5O1gcCcr7wgc5dRpiPHoCWq1tJQ9r/nzliSCRZE9bgM7r1KoEKypFDGp
XDn9yyXfORScmWKwMQFvGEPIXodu52+DXd4blyYaDdlURXpVLWPItZVfPi4+vV8Ux0V6KH1dmwq0
r5YtV6DAEEFpbACotUVai1YZt+c+4p+CUaZleGGQ7nxJ+AQH8f8GmZ7/R0gyJwyRNhbwwqfdip5y
blQE/U6RJ+PiAD+1j3Fw30yjD8gTFOH8KuxHpMvOAwtBJAOHIelrtoMjT9/5hr+6Hbr51qB7vGW6
T8bfAAAA//8DAFBLAwQUAAYACAAAACEAtVUwI/UAAABMAgAACwAIAl9yZWxzLy5yZWxzIKIEAiig
AAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
AIySz07DMAzG70i8Q+T76m5ICKGlu0xIuyFUHsAk7h+1jaMkQPf2hAOCSmPb0fbnzz9b3u7maVQf
HGIvTsO6KEGxM2J712p4rZ9WD6BiImdpFMcajhxhV93ebF94pJSbYtf7qLKLixq6lPwjYjQdTxQL
8exypZEwUcphaNGTGahl3JTlPYa/HlAtPNXBaggHeweqPvo8+bK3NE1veC/mfWKXToxAnhM7y3bl
Q2YLqc/bqJpCy0mDFfOc0xHJ+yJjA54m2lxP9P+2OHEiS4nQSODzPN+Kc0Dr64Eun2ip+L3OPOKn
hOFNZPhhwcUPVF8AAAD//wMAUEsDBBQABgAIAAAAIQCBPpSX9AAAALoCAAAaAAgBeGwvX3JlbHMv
d29ya2Jvb2sueG1sLnJlbHMgogQBKKAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
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";

        #endregion

        private bool _isClosing;
        private bool _isClosed;

        private SpreadsheetDocument _spreadSheet;
        private WorkbookPart _workbookPart;
        private Stream _stream;
        private string _origninalSheetId;
        private string _replacementPartId;
        private OpenXmlReader _reader;
        private OpenXmlWriter _writer;

        public void Stream(Stream stream)
        {
            _stream = stream;
            InitiateWorkbook();
        }

        public void WriteHeaders(string[] headers)
        {
            WriteRow(headers);
        }

        public void WriteRow(string[] row)
        {
            var r  = new Row();
            _writer.WriteStartElement(r);
            foreach (var column in row)
            {
                var cell = new Cell();
                var cellValue = new CellValue(column);
                cell.DataType = CellValues.String;
                cell.AppendChild<OpenXmlElement>(cellValue);
                _writer.WriteElement(cell);
            }
            _writer.WriteEndElement();
        }

        public void Close()
        {
            if (!_isClosed || !_isClosing)
            {
                _isClosing = true;
                while (_reader.Read())
                {
                    if (_reader.IsStartElement)
                    {
                        _writer.WriteStartElement(_reader);
                    }
                    else if (_reader.IsEndElement)
                    {
                        _writer.WriteEndElement();
                    }
                }
                _reader.Close();
                _writer.Close();

                var sheet =
                    _workbookPart.Workbook.Descendants<Sheet>().First(s => s.Id.Value.Equals(_origninalSheetId));
                sheet.Id.Value = _replacementPartId;
                _workbookPart.DeletePart(_workbookPart.WorksheetParts.First());

                _spreadSheet.Close();
                _stream.Close();
                _isClosing = false;
            }
            _isClosed = true;
        }

        public void Dispose()
        {
            Close();
        }

        private void InitiateWorkbook()
        {
            var base64CharArray = EmptyXlsx
                .Where(c => c != '\r' && c != '\n').ToArray();
            var byteArray =
                Convert.FromBase64CharArray(base64CharArray,
                                            0, base64CharArray.Length);
            _stream.Write(byteArray, 0, byteArray.Length);
            _stream.Position = 0;

            _spreadSheet = SpreadsheetDocument.Open(_stream, true);

            _workbookPart = _spreadSheet.WorkbookPart;
            OpenXmlPart worksheetPart = _workbookPart.WorksheetParts.First();
            _origninalSheetId = _workbookPart.GetIdOfPart(worksheetPart);

            var replacementPart = _workbookPart.AddNewPart<WorksheetPart>();
            _replacementPartId = _workbookPart.GetIdOfPart(replacementPart);

            _reader = OpenXmlReader.Create(worksheetPart);
            _writer = OpenXmlWriter.Create(replacementPart);

            while(_reader.Read())
            {
                if(_reader.ElementType == typeof(SheetData))
                {
                    if(_reader.IsEndElement)
                    {
                        continue;
                    }
                    _writer.WriteStartElement(new SheetData());
                    break;
                }
                if (_reader.IsStartElement)
                {
                    _writer.WriteStartElement(_reader);
                }
                else if (_reader.IsEndElement)
                {
                    _writer.WriteEndElement();
                }
            }
        }
    }
}
