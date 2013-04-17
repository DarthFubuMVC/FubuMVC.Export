using FubuCore;
using FubuTestingSupport;
using NUnit.Framework;

namespace FubuMVC.Export.Tests
{
    [TestFixture]
    public class TemporaryFileStorageTester
    {
        private TemporaryFileStorage theTemporaryFileStorage;

        [SetUp]
        public void Setup()
        {
            theTemporaryFileStorage = new TemporaryFileStorage();
        }

        [Test]
        public void create_should_return_a_unique_file_name()
        {
            var filename1 = theTemporaryFileStorage.Create();
            var filename2 = theTemporaryFileStorage.Create();

            filename1.ShouldNotEqual(filename2);
        }

        [Test]
        public void open_should_open_the_given_temp_file_path_for_writing()
        {
            var tempfile = theTemporaryFileStorage.Create();
            Assert.DoesNotThrow(() =>
            {
                using (var theStream = theTemporaryFileStorage.Open(tempfile))
                {
                    theStream.ShouldNotBeNull();
                    theStream.WriteByte(1);
                }
            });
        }

        [Test]
        public void should_return_filename_from_hash()
        {
            var filename1 = theTemporaryFileStorage.Create();
            theTemporaryFileStorage.FileNameFromHash(filename1.ToHash()).ShouldEqual(filename1);
        }
    }
}