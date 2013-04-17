using System.IO;
using FubuCore;
using FubuCore.Formatting;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Export.Tests
{
	[TestFixture]
	public class ExcelWriterFactoryTester : InteractionContext<ExcelWriterFactory<ExcelWriterFactoryTester.MyExcelWriter>>
	{
		private const string thePath = @"c:\something.xlsx";
		private ITemporaryFileStorage theTemporaryFileStorage;
		private MemoryStream theMemoryStream;

		protected override void beforeEach()
		{
			theMemoryStream = new MemoryStream(0);

			theTemporaryFileStorage = MockFor<ITemporaryFileStorage>();
			theTemporaryFileStorage.Stub(s => s.Create()).Return(thePath);
			theTemporaryFileStorage.Stub(s => s.Open(Arg<string>.Is.Equal(thePath))).Return(theMemoryStream);
		}

		[TearDown]
		public void TearDown()
		{
			if (theMemoryStream != null)
			{
				theMemoryStream.Dispose();
			}
		}

		[Test]
		public void build_should_call_stream()
		{
			var response = ClassUnderTest.Build();
			response.Writer.As<MyExcelWriter>().TheStream.ShouldNotBeNull();
			response.Writer.As<MyExcelWriter>().TheStream.ShouldEqual(theMemoryStream);
		}

		[Test]
		public void build_should_call_create_and_open_on_the_stream_provider()
		{
			ClassUnderTest.Build();
			theTemporaryFileStorage.AssertWasCalled(s => s.Create());
			theTemporaryFileStorage.AssertWasCalled(s => s.Open(Arg<string>.Is.Equal(thePath)));
		}

		public class MyExcelWriter : IExcelWriter
		{
			public Stream TheStream { get; set; }

			public void Dispose()
			{
				if (TheStream != null)
				{
					TheStream.Dispose();
				}
			}

			public void Stream(Stream stream)
			{
				TheStream = stream;
			}

			public void WriteHeaders(string[] headers)
			{
			}

			public void WriteRow(string[] values)
			{
			}
		}
	}
}
