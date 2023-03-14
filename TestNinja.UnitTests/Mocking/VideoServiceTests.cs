using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        [SetUp]
        public void SetUp()
        {
            _service = new VideoService();
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _service.FileReader = new FakeFileReader();
            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error parsing the video.").IgnoreCase);
        }
    }
}
