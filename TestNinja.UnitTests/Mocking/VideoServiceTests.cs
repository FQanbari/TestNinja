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
        private Mock<IVideoRepository> _repository;
        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IFileReader>();            
            _repository = new Mock<IVideoRepository>();            
            _service = new VideoService(_mock.Object,_repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _mock.Setup(m => m.Read("video.txt")).Returns("");
            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("Error parsing the video.").IgnoreCase);
        }
        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnEmptyList()
        {
            _repository.Setup(r => r.GetUnProssedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }
        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnProcessed_ReturnAStringWithIdOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnProssedVideos()).Returns(new List<Video>() 
            { 
                new Video{Id = 1} ,
                new Video{Id = 2} ,
                new Video{Id = 3} ,
            });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
