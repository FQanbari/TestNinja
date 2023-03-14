using Moq;
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
        private Mock<IFileReader> _mock;
        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IFileReader>();            
            _service = new VideoService(_mock.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mock.Setup(m => m.Read("video.txt")).Returns("");
            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error parsing the video.").IgnoreCase);
        }
    }
}
